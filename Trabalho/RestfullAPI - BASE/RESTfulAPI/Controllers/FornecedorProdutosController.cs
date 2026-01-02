using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Constants;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;
using System.Security.Claims;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/fornecedor/produtos")]
[Authorize(Roles = Roles.Fornecedor)]
public class FornecedorProdutosController(AppDbContext db) : ControllerBase
{
    string UserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("UserId não encontrado no token.");

    public record UpsertProdutoDto(
        string Nome,
        string Descricao,
        decimal PrecoBase,
        int ModoDisponibilizacaoId,
        int Stock,
        List<int> CategoriaIds,
        string? UrlImagem
    );

    // GET /api/fornecedor/produtos
    [HttpGet]
    public async Task<IActionResult> Meus()
    {
        var uid = UserId();

        var produtos = await db.Produtos.AsNoTracking()
            .Where(p => p.FornecedorId == uid)
            .OrderByDescending(p => p.ProdutoId)
            .Select(p => new
            {
                p.ProdutoId,
                p.Nome,
                p.Descricao,
                p.PrecoBase,
                p.PrecoFinal,
                p.PercentagemComissao,
                p.Estado,
                p.ModoDisponibilizacaoId,
                p.Stock,
                p.UrlImagem,
                CategoriaIds = p.CategoriaProdutos.Select(cp => cp.CategoriaId).ToList()
            })
            .ToListAsync();

        return Ok(produtos);
    }

    // POST /api/fornecedor/produtos
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] UpsertProdutoDto dto)
    {
        var uid = UserId();

        var produto = new Produto
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            PrecoBase = dto.PrecoBase,
            ModoDisponibilizacaoId = dto.ModoDisponibilizacaoId,
            Stock = dto.Stock,
            UrlImagem = dto.UrlImagem,

            FornecedorId = uid,
            Estado = EstadoProduto.Pendente,

            // normalmente são calculados/ajustados na Gestão-Loja
            PrecoFinal = 0m,
            PercentagemComissao = 0m
        };

        foreach (var catId in dto.CategoriaIds.Distinct())
            produto.CategoriaProdutos.Add(new CategoriaProduto { CategoriaId = catId });

        db.Produtos.Add(produto);
        await db.SaveChangesAsync();

        return Ok(new { produto.ProdutoId, produto.Estado });
    }

    // PUT /api/fornecedor/produtos/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar(int id, [FromBody] UpsertProdutoDto dto)
    {
        var uid = UserId();

        var produto = await db.Produtos
            .Include(p => p.CategoriaProdutos)
            .FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto is null) return NotFound();
        if (produto.FornecedorId != uid) return Forbid();

        produto.Nome = dto.Nome;
        produto.Descricao = dto.Descricao;
        produto.PrecoBase = dto.PrecoBase;
        produto.ModoDisponibilizacaoId = dto.ModoDisponibilizacaoId;
        produto.Stock = dto.Stock;
        produto.UrlImagem = dto.UrlImagem;

        // regra: editar => volta a pendente
        produto.Estado = EstadoProduto.Pendente;

        produto.CategoriaProdutos.Clear();
        foreach (var catId in dto.CategoriaIds.Distinct())
            produto.CategoriaProdutos.Add(new CategoriaProduto { ProdutoId = produto.ProdutoId, CategoriaId = catId });

        await db.SaveChangesAsync();
        return Ok(new { produto.ProdutoId, produto.Estado });
    }

    // DELETE /api/fornecedor/produtos/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var uid = UserId();

        var produto = await db.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (produto is null) return NotFound();
        if (produto.FornecedorId != uid) return Forbid();

        // soft delete (mais seguro)
        produto.Estado = EstadoProduto.Inativo;
        await db.SaveChangesAsync();

        return Ok(new { produto.ProdutoId, produto.Estado });
    }
}
