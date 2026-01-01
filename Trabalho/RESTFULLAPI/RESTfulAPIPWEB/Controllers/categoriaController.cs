using RESTfulAPIPWEB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RESTfulAPIPWEB.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int? tipoCategoriaId = null)
        {
            var categorias = await _categoriaRepository.GetCategorias(tipoCategoriaId);
            return Ok(categorias);
        }
    }
}
