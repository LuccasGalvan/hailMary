using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWEB.Data;
using RESTfulAPIPWEB.DTO;
using RESTfulAPIPWEB.Entity;
using RESTfulAPIPWEB.Entity.Enums;

namespace RESTfulAPIPWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CarrinhoCompras/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CarrinhoItemDto>>> GetCarrinho(string userId)
        {
            var carrinho = await _context.CarrinhoCompras
                .Include(c => c.Produto)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!carrinho.Any())
            {
                return NotFound("Nenhum item encontrado no carrinho.");
            }

            var carrinhoDto = carrinho.Select(item => new CarrinhoItemDto
            {
                Id = item.Id,
                ProdutoId = item.ProdutoId,
                Produto = item.Produto == null ? null : MapProduto(item.Produto),
                UserId = item.UserId,
                Quantidade = item.Quantidade
            }).ToList();

            return Ok(carrinhoDto);
        }

        // PUT: api/CarrinhoCompras/limpar/{userId}
        [HttpPut("limpar/{userId}")]
        public async Task<IActionResult> LimparCarrinho(string userId)
        {
            var itensCarrinho = await _context.CarrinhoCompras.Where(c => c.UserId == userId).ToListAsync();

            if (itensCarrinho == null || itensCarrinho.Count == 0)
            {
                return NotFound("Carrinho já está vazio ou não encontrado.");
            }

            _context.CarrinhoCompras.RemoveRange(itensCarrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/CarrinhoCompras/{produtoId}/{acao}?userId=...&quantidade=...
        [HttpPut("{produtoId}/{acao}")]
        public async Task<IActionResult> AtualizaCarrinho(int produtoId, string acao, [FromQuery] string userId, [FromQuery] int quantidade)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return BadRequest("O ID do usuário é obrigatório.");

                if (quantidade <= 0)
                    return BadRequest("A quantidade deve ser maior que 0.");

                var produto = await _context.Produtos.FindAsync(produtoId);
                if (produto == null)
                    return NotFound("Produto não encontrado.");

                // Only allow cart operations on products that are visible/sellable
                if (produto.Estado != ProdutoEstado.Activo || produto.PrecoFinal == null)
                    return BadRequest("Produto não disponível para compra.");

                var itemExistente = await _context.CarrinhoCompras
                    .FirstOrDefaultAsync(c => c.ProdutoId == produtoId && c.UserId == userId);

                switch (acao.ToLowerInvariant())
                {
                    case "adicionar":
                        {
                            if (itemExistente != null)
                            {
                                var novaQuantidade = itemExistente.Quantidade + quantidade;
                                if (novaQuantidade > produto.Stock)
                                    return BadRequest("Quantidade excede o stock disponível.");

                                itemExistente.Quantidade = novaQuantidade;
                                _context.Entry(itemExistente).State = EntityState.Modified;
                            }
                            else
                            {
                                if (quantidade > produto.Stock)
                                    return BadRequest("Quantidade excede o stock disponível.");

                                var novoItem = new CarrinhoCompras
                                {
                                    UserId = userId,
                                    ProdutoId = produtoId,
                                    Quantidade = quantidade,
                                };
                                _context.CarrinhoCompras.Add(novoItem);
                            }

                            break;
                        }

                    case "remover":
                        {
                            if (itemExistente == null)
                                return NotFound("O item não existe no carrinho.");

                            if (quantidade >= itemExistente.Quantidade)
                            {
                                _context.CarrinhoCompras.Remove(itemExistente);
                            }
                            else
                            {
                                itemExistente.Quantidade -= quantidade;
                                _context.Entry(itemExistente).State = EntityState.Modified;
                            }

                            break;
                        }

                    default:
                        return BadRequest("Ação inválida. Use 'adicionar' ou 'remover'.");
                }

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        private static ProdutoDto MapProduto(Produto produto)
        {
            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Detalhe = produto.Descricao,
                Origem = produto.Origem,
                Titulo = string.Empty,
                UrlImagem = produto.UrlImagem,
                Preco = produto.PrecoFinal ?? produto.PrecoBase,
                Promocao = produto.Promocao,
                MaisVendido = produto.MaisVendido,
                EmStock = produto.Stock,
                Disponivel = produto.ParaVenda,
                ModoEntregaId = produto.ModoEntregaId,
                modoentrega = produto.modoentrega,
                ModoDisponibilizacaoId = produto.ModoDisponibilizacaoId,
                ModoDisponibilizacao = produto.ModoDisponibilizacao,
                CategoriaId = produto.CategoriaId,
                categoria = produto.categoria,
                Imagem = produto.Imagem,
            };
        }
    }
}
