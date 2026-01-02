using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModosDisponibilizacaoController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] bool? ativo = true)
    {
        var q = db.ModosDisponibilizacao.AsNoTracking();

        if (ativo.HasValue)
            q = q.Where(m => m.Ativo == ativo.Value);

        var modos = await q
            .OrderBy(m => m.ModoDisponibilizacaoId)
            .Select(m => new { m.ModoDisponibilizacaoId, m.Nome, m.IsForSale, m.Ativo })
            .ToListAsync();

        return Ok(modos);
    }
}
