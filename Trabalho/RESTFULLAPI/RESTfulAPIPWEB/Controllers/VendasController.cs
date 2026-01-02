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
        public async Task<ActionResult<VendaCreateResponse>> Criar([FromBody] CreateVendaRequest request)
        {
            var userId = GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            if (request.Linhas.Count == 0)
                return BadRequest("Linhas obrigatórias.");

            if (request.Linhas.Any(l => l.Quantidade <= 0))
                return BadRequest("Quantidade inválida.");

            var linhasAgrupadas = request.Linhas
                .GroupBy(l => l.ProdutoId)
                .Select(g => new { ProdutoId = g.Key, Quantidade = g.Sum(l => l.Quantidade) })
                .ToList();

            if (linhasAgrupadas.Any(l => l.Quantidade <= 0))
                return BadRequest("Quantidade inválida.");

            var produtoIds = linhasAgrupadas.Select(l => l.ProdutoId).Distinct().ToList();
            var produtos = await _context.Produtos
                .Include(p => p.ModoDisponibilizacao)
                .Where(p => produtoIds.Contains(p.ProdutoId))
                .ToDictionaryAsync(p => p.ProdutoId);

            foreach (var linha in linhasAgrupadas)
            {
                if (!produtos.TryGetValue(linha.ProdutoId, out var produto))
                    return BadRequest($"Produto inválido: {linha.ProdutoId}");

                if (produto.Estado != ProdutoEstado.Activo || produto.PrecoFinal == null)
                    return BadRequest($"Produto não disponível: {produto.Nome}");

                if (produto.ModoDisponibilizacao == null || !produto.ModoDisponibilizacao.IsForSale)
                    return BadRequest($"Produto não disponível: {produto.Nome}");

                if (produto.Stock < linha.Quantidade)
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

            foreach (var linha in linhasAgrupadas)
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
        public async Task<ActionResult<VendaPagamentoResponse>> Pagar(int id, [FromBody] PagarVendaRequest? request)
        {
            if (request == null)
                return BadRequest("Dados de pagamento obrigatórios.");

            var encomenda = await _context.Encomendas
                .Include(e => e.Linhas)
                .ThenInclude(l => l.Produto)
                .ThenInclude(p => p!.ModoDisponibilizacao)
                .FirstOrDefaultAsync(e => e.VendaId == id);

            if (encomenda == null)
                return NotFound();

            if (encomenda.Estado != EncomendaEstado.PendentePagamento)
                return BadRequest("Encomenda não está pendente de pagamento.");

            foreach (var linha in encomenda.Linhas)
            {
                if (linha.Produto == null)
                    return BadRequest($"Produto inválido: {linha.ProdutoId}");

                if (linha.Produto.Estado != ProdutoEstado.Activo || linha.Produto.PrecoFinal == null)
                    return BadRequest($"Produto não disponível: {linha.Produto.Nome}");

                if (linha.Produto.ModoDisponibilizacao == null || !linha.Produto.ModoDisponibilizacao.IsForSale)
                    return BadRequest($"Produto não disponível: {linha.Produto.Nome}");

                if (linha.Produto.Stock < linha.Quantidade)
                    return BadRequest($"Stock insuficiente para: {linha.Produto.Nome}");
            }

            foreach (var linha in encomenda.Linhas)
            {
                linha.Produto!.Stock -= linha.Quantidade;
            }

            encomenda.Estado = EncomendaEstado.Paga;
            encomenda.PagamentoExecutado = true;
            encomenda.DataPagamento = DateTime.Now;
            encomenda.MetodoPagamento = request.MetodoPagamento;
            encomenda.Observacoes = request.Observacoes;
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
