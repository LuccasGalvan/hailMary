using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWEB.Data;

namespace RESTfulAPIPWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModosDisponibilizacaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModosDisponibilizacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool? ativo = true)
        {
            var query = _context.ModosDisponibilizacao.AsNoTracking();

            if (ativo.HasValue)
            {
                query = query.Where(m => m.Ativo == ativo.Value);
            }

            var modos = await query
                .OrderBy(m => m.ModoDisponibilizacaoId)
                .Select(m => new
                {
                    m.ModoDisponibilizacaoId,
                    m.Nome,
                    m.IsForSale,
                    m.Ativo
                })
                .ToListAsync();

            return Ok(modos);
        }
    }
}
