using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoCategoriasController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tipos = await db.TipoCategorias
            .AsNoTracking()
            .OrderBy(t => t.Nome)
            .Select(t => new { t.TipoCategoriaId, t.Nome })
            .ToListAsync();

        return Ok(tipos);
    }
}
