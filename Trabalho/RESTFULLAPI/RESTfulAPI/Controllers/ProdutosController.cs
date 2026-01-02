using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Constants;
using RESTfulAPI.Data;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController(AppDbContext db, IConfiguration config) : ControllerBase
{
    // GET /api/Produtos?categoriaId=13&modoDisponibilizacaoId=1&texto=metallica&soAtivos=true
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int? categoriaId = null,
        [FromQuery] int? modoDisponibilizacaoId = null,
        [FromQuery] string? texto = null,
        [FromQuery] bool soAtivos = true
    )
    {
        var q = db.Produtos.AsNoTracking();

        if (soAtivos)
            q = q.Where(p => p.Estado == EstadoProduto.Ativo);

        if (modoDisponibilizacaoId.HasValue)
            q = q.Where(p => p.ModoDisponibilizacaoId == modoDisponibilizacaoId.Value);

        if (!string.IsNullOrWhiteSpace(texto))
            q = q.Where(p => p.Nome.Contains(texto) || p.Descricao.Contains(texto));

        if (categoriaId.HasValue)
        {
            q = q.Where(p => p.CategoriaProdutos.Any(cp => cp.CategoriaId == categoriaId.Value));
        }

        var imageBaseUrl = config["Images:BaseUrl"]!;

        var produtos = await q
            .OrderBy(p => p.Nome)
            .Select(p => new
            {
                p.ProdutoId,
                p.Nome,
                p.Descricao,
                p.PrecoFinal,
                p.ModoDisponibilizacaoId,
                p.Stock,
                UrlImagem = string.IsNullOrWhiteSpace(p.UrlImagem)
                            ? imageBaseUrl + "/images/produtos/no-image.png"
    :                       (p.UrlImagem.StartsWith("/") ? imageBaseUrl + p.UrlImagem : p.UrlImagem),


                CategoriaIds = p.CategoriaProdutos.Select(cp => cp.CategoriaId).ToList()
            })
            .ToListAsync();

        return Ok(produtos);
    }

    // GET /api/Produtos/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {

        var imageBaseUrl = config["Images:BaseUrl"]!;

        var p = await db.Produtos.AsNoTracking()
            .Where(x => x.ProdutoId == id && x.Estado == EstadoProduto.Ativo)
            .Select(x => new
            {
                x.ProdutoId,
                x.Nome,
                x.Descricao,
                x.PrecoFinal,
                x.ModoDisponibilizacaoId,
                x.Stock,
                UrlImagem = string.IsNullOrWhiteSpace(x.UrlImagem)
                            ? imageBaseUrl + "/images/produtos/no-image.png"
    :                       (x.UrlImagem.StartsWith("/") ? imageBaseUrl + x.UrlImagem : x.UrlImagem),

                CategoriaIds = x.CategoriaProdutos.Select(cp => cp.CategoriaId).ToList()
            })
            .FirstOrDefaultAsync();

        if (p is null) return NotFound();
        return Ok(p);
    }
}
