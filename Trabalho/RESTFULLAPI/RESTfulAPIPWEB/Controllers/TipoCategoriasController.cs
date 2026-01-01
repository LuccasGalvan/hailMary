using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWEB.Data;
using RESTfulAPIPWEB.DTO;

namespace RESTfulAPIPWEB.Controllers
{
    [Route("api/TipoCategorias")]
    [ApiController]
    [Authorize]
    public class TipoCategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TipoCategoriasController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var tipos = await _context.TiposCategoria
                .AsNoTracking()
                .OrderBy(t => t.Nome)
                .Select(t => new TipoCategoriaDto
                {
                    Id = t.Id,
                    Nome = t.Nome
                })
                .ToListAsync();

            return Ok(tipos);
        }
    }
}
