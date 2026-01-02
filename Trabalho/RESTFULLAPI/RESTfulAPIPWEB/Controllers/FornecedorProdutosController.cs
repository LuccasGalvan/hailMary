using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWEB.Constants;
using RESTfulAPIPWEB.Data;
using RESTfulAPIPWEB.DTO.Fornecedor;
using RESTfulAPIPWEB.Entity;
using RESTfulAPIPWEB.Entity.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RESTfulAPIPWEB.Controllers
{
    [Route("api/[controller]")]
    [Route("api/fornecedor/produtos")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.Fornecedor)]
    public class FornecedorProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FornecedorProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorProdutoDto>>> GetProdutos()
        {
            var fornecedorId = GetFornecedorId();
            if (string.IsNullOrWhiteSpace(fornecedorId))
                return Unauthorized();

            var produtos = await _context.Produtos
                .AsNoTracking()
                .Where(p => p.FornecedorId == fornecedorId)
                .OrderBy(p => p.Nome)
                .Select(p => new FornecedorProdutoDto
                {
                    Id = p.ProdutoId,
                    Nome = p.Nome,
                    Detalhe = p.Descricao,
                    UrlImagem = p.UrlImagem,
                    Imagem = p.Imagem,
                    PrecoBase = p.PrecoBase,
                    MargemPercentual = p.PercentagemComissao,
                    PrecoFinal = p.PrecoFinal,
                    Estado = p.Estado,
                    Promocao = p.Promocao,
                    MaisVendido = p.MaisVendido,
                    EmStock = p.Stock,
                    ParaVenda = p.ParaVenda,
                    Origem = p.Origem,
                    CategoriaId = p.CategoriaId,
                    CategoriaIds = p.CategoriaProdutos.Select(cp => cp.CategoriaId).ToList(),
                    ModoEntregaId = p.ModoEntregaId,
                    ModoDisponibilizacaoId = p.ModoDisponibilizacaoId
                })
                .ToListAsync();

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorProdutoDto>> CreateProduto([FromBody] FornecedorProdutoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var fornecedorId = GetFornecedorId();
            if (string.IsNullOrWhiteSpace(fornecedorId))
                return Unauthorized();

            var categoriaIds = NormalizeCategoriaIds(dto.CategoriaIds, dto.CategoriaId);
            if (categoriaIds.Count == 0)
            {
                ModelState.AddModelError(nameof(dto.CategoriaIds), "Pelo menos uma categoria é necessária.");
                return ValidationProblem(ModelState);
            }

            var produto = new Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Detalhe,
                UrlImagem = dto.UrlImagem,
                Imagem = dto.Imagem,
                PrecoBase = dto.PrecoBase,
                PercentagemComissao = dto.MargemPercentual,
                Estado = ProdutoEstado.Pendente,
                PrecoFinal = null,
                FornecedorId = fornecedorId,
                Promocao = dto.Promocao,
                MaisVendido = dto.MaisVendido,
                Stock = dto.EmStock,
                ParaVenda = dto.ParaVenda,
                Origem = dto.Origem,
                CategoriaId = categoriaIds[0],
                ModoEntregaId = dto.ModoEntregaId,
                ModoDisponibilizacaoId = dto.ModoDisponibilizacaoId
            };

            foreach (var categoriaId in categoriaIds)
            {
                produto.CategoriaProdutos.Add(new CategoriaProduto { CategoriaId = categoriaId });
            }

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var produtoDto = MapProdutoDto(produto);

            return Created($"/api/FornecedorProdutos/{produto.Id}", new
            {
                ProdutoId = produto.Id,
                Estado = produto.Estado,
                Produto = produtoDto
            });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FornecedorProdutoDto>> UpdateProduto(int id, [FromBody] FornecedorProdutoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var fornecedorId = GetFornecedorId();
            if (string.IsNullOrWhiteSpace(fornecedorId))
                return Unauthorized();

            var produto = await _context.Produtos
                .Include(p => p.CategoriaProdutos)
                .FirstOrDefaultAsync(p => p.ProdutoId == id && p.FornecedorId == fornecedorId);

            if (produto is null)
                return NotFound();

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Detalhe;
            produto.UrlImagem = dto.UrlImagem;
            if (dto.Imagem is not null)
                produto.Imagem = dto.Imagem;
            produto.PrecoBase = dto.PrecoBase;
            produto.PercentagemComissao = dto.MargemPercentual;
            produto.Promocao = dto.Promocao;
            produto.MaisVendido = dto.MaisVendido;
            produto.Stock = dto.EmStock;
            produto.ParaVenda = dto.ParaVenda;
            produto.Origem = dto.Origem;
            var categoriaIds = NormalizeCategoriaIds(dto.CategoriaIds, dto.CategoriaId);
            if (categoriaIds.Count == 0)
            {
                ModelState.AddModelError(nameof(dto.CategoriaIds), "Pelo menos uma categoria é necessária.");
                return ValidationProblem(ModelState);
            }

            produto.CategoriaId = categoriaIds[0];
            produto.ModoEntregaId = dto.ModoEntregaId;
            produto.ModoDisponibilizacaoId = dto.ModoDisponibilizacaoId;
            produto.Estado = ProdutoEstado.Pendente;
            produto.PrecoFinal = null;

            produto.CategoriaProdutos.Clear();
            foreach (var categoriaId in categoriaIds)
            {
                produto.CategoriaProdutos.Add(new CategoriaProduto
                {
                    ProdutoId = produto.ProdutoId,
                    CategoriaId = categoriaId
                });
            }

            await _context.SaveChangesAsync();

            var produtoDto = MapProdutoDto(produto);

            return Ok(new
            {
                ProdutoId = produto.Id,
                Estado = produto.Estado,
                Produto = produtoDto
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var fornecedorId = GetFornecedorId();
            if (string.IsNullOrWhiteSpace(fornecedorId))
                return Unauthorized();

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.ProdutoId == id && p.FornecedorId == fornecedorId);

            if (produto is null)
                return NotFound();

            produto.Estado = ProdutoEstado.Inactivo;
            await _context.SaveChangesAsync();

            return Ok(new { produto.ProdutoId, produto.Estado });
        }

        [HttpGet("vendas")]
        public async Task<ActionResult<IEnumerable<FornecedorVendaDto>>> GetVendas()
        {
            var fornecedorId = GetFornecedorId();
            if (string.IsNullOrWhiteSpace(fornecedorId))
                return Unauthorized();

            var vendas = await _context.EncomendaItens
                .AsNoTracking()
                .Where(i => i.Produto != null && i.Produto.FornecedorId == fornecedorId)
                .Include(i => i.Produto)
                .Include(i => i.Encomenda)
                .OrderByDescending(i => i.Encomenda!.DataCriacao)
                .Select(i => new FornecedorVendaDto
                {
                    EncomendaId = i.VendaId,
                    EncomendaCriadaEmUtc = i.Encomenda!.DataCriacao,
                    EncomendaEstado = i.Encomenda.Estado,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto!.Nome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.TotalLinha
                })
                .ToListAsync();

            return Ok(vendas);
        }

        private string? GetFornecedorId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private static FornecedorProdutoDto MapProdutoDto(Produto produto)
        {
            var categoriaIds = produto.CategoriaProdutos
                .Select(cp => cp.CategoriaId)
                .ToList();

            if (categoriaIds.Count == 0 && produto.CategoriaId > 0)
            {
                categoriaIds.Add(produto.CategoriaId);
            }

            return new FornecedorProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Detalhe = produto.Descricao,
                UrlImagem = produto.UrlImagem,
                Imagem = produto.Imagem,
                PrecoBase = produto.PrecoBase,
                MargemPercentual = produto.PercentagemComissao,
                PrecoFinal = produto.PrecoFinal,
                Estado = produto.Estado,
                Promocao = produto.Promocao,
                MaisVendido = produto.MaisVendido,
                EmStock = produto.Stock,
                ParaVenda = produto.ParaVenda,
                Origem = produto.Origem,
                CategoriaId = produto.CategoriaId,
                CategoriaIds = categoriaIds,
                ModoEntregaId = produto.ModoEntregaId,
                ModoDisponibilizacaoId = produto.ModoDisponibilizacaoId
            };
        }

        private static List<int> NormalizeCategoriaIds(List<int>? categoriaIds, int? categoriaId)
        {
            var ids = categoriaIds?
                .Where(id => id > 0)
                .Distinct()
                .ToList() ?? new List<int>();

            if (ids.Count == 0 && categoriaId is > 0)
            {
                ids.Add(categoriaId.Value);
            }

            return ids;
        }
    }
}
