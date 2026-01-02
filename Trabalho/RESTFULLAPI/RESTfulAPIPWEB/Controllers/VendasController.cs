using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWEB.Data;
using RESTfulAPIPWEB.DTO.Vendas;
using RESTfulAPIPWEB.Entity;
using RESTfulAPIPWEB.Entity.Enums;

namespace RESTfulAPIPWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Cliente")]
    public class VendasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Vendas
        [HttpPost]
        [ProducesResponseType(typeof(VendaCreateResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VendaCreateResponse>> Criar([FromBody] VendaCreateRequest request)
        {
            var userId = GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            if (request.Linhas.Count == 0)
                return BadRequest("Linhas obrigatórias.");

            var produtoIds = request.Linhas.Select(l => l.ProdutoId).Distinct().ToList();
            var produtos = await _context.Produtos
                .Where(p => produtoIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            foreach (var linha in request.Linhas)
            {
                if (!produtos.TryGetValue(linha.ProdutoId, out var produto))
                    return BadRequest($"Produto inválido: {linha.ProdutoId}");

                if (produto.Estado != ProdutoEstado.Activo || produto.PrecoFinal == null)
                    return BadRequest($"Produto não disponível: {produto.Nome}");

                if (linha.Quantidade <= 0)
                    return BadRequest($"Quantidade inválida para: {produto.Nome}");

                if (produto.EmStock < linha.Quantidade)
                    return BadRequest($"Stock insuficiente para: {produto.Nome}");
            }

            await using var tx = await _context.Database.BeginTransactionAsync();

            var encomenda = new Encomenda
            {
                ClienteId = userId,
                Estado = EncomendaEstado.PendentePagamento
            };

            _context.Encomendas.Add(encomenda);

            decimal total = 0m;

            foreach (var linha in request.Linhas)
            {
                var produto = produtos[linha.ProdutoId];
                var precoUnit = produto.PrecoFinal!.Value;
                var subtotal = precoUnit * linha.Quantidade;

                _context.EncomendaItens.Add(new EncomendaItem
                {
                    Encomenda = encomenda,
                    ProdutoId = produto.Id,
                    Quantidade = linha.Quantidade,
                    PrecoUnitario = precoUnit,
                    TotalLinha = subtotal
                });

                produto.EmStock -= linha.Quantidade;
                total += subtotal;
            }

            encomenda.ValorTotal = total;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            var response = new VendaCreateResponse
            {
                VendaId = encomenda.VendaId
            };

            return CreatedAtAction(nameof(GetMinhas), null, response);
        }

        // GET: api/Vendas/minhas
        [HttpGet("minhas")]
        public async Task<ActionResult<IEnumerable<VendaMinhasDto>>> GetMinhas()
        {
            var userId = GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var vendas = await _context.Encomendas
                .AsNoTracking()
                .Where(e => e.ClienteId == userId)
                .Include(e => e.Linhas)
                .OrderByDescending(e => e.DataCriacao)
                .Select(e => new VendaMinhasDto
                {
                    VendaId = e.VendaId,
                    DataCriacao = e.DataCriacao,
                    Estado = e.Estado,
                    ValorTotal = e.ValorTotal,
                    PagamentoExecutado = e.PagamentoExecutado,
                    Linhas = e.Linhas.Select(i => new VendaLinhaDto
                    {
                        ProdutoId = i.ProdutoId,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario,
                        TotalLinha = i.TotalLinha
                    }).ToList()
                })
                .ToListAsync();

            return Ok(vendas);
        }

        // POST: api/Vendas/{id}/pagar
        [HttpPost("{id:int}/pagar")]
        [ProducesResponseType(typeof(VendaPagamentoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VendaPagamentoResponse>> Pagar(int id)
        {
            var encomenda = await _context.Encomendas.FirstOrDefaultAsync(e => e.VendaId == id);

            if (encomenda == null)
                return NotFound();

            if (encomenda.Estado != EncomendaEstado.PendentePagamento)
                return BadRequest("Encomenda não está pendente de pagamento.");

            encomenda.Estado = EncomendaEstado.Paga;
            encomenda.PagamentoExecutado = true;
            encomenda.DataPagamento = DateTime.Now;
            await _context.SaveChangesAsync();

            var response = new VendaPagamentoResponse
            {
                VendaId = encomenda.VendaId,
                Estado = encomenda.Estado,
                PagoEmUtc = encomenda.DataPagamento ?? DateTime.Now
            };

            return Ok(response);
        }

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
