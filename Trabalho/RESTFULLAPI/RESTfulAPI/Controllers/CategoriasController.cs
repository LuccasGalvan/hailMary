using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? tipoCategoriaId = null)
    {
        var q = db.Categorias.AsNoTracking();

        if (tipoCategoriaId.HasValue)
            q = q.Where(c => c.TipoCategoriaId == tipoCategoriaId.Value);

        var categorias = await q
            .OrderBy(c => c.Nome)
            .Select(c => new
            {
                c.CategoriaId,
                c.Nome,
                c.TipoCategoriaId,
                c.UrlImagem
            })
            .ToListAsync();

        return Ok(categorias);
    }
}
