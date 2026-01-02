using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWEB.Data;
using RESTfulAPIPWEB.Entity;

namespace RESTfulAPIPWEB.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Categoria>> GetCategorias(int? tipoCategoriaId = null)
        {
            var query = _context.Categorias
                .AsNoTracking();

            if (tipoCategoriaId.HasValue)
            {
                query = query.Where(c => c.TipoCategoriaId == tipoCategoriaId.Value);
            }

            var categorias = await query
                .Include(c => c.Children)
                .ThenInclude(c => c.Children)
                .OrderBy(c => c.Ordem)
                .ThenBy(c => c.Nome)
                .ToListAsync();

            foreach (var categoria in categorias)
            {
                OrderChildren(categoria);
            }

            return categorias;
        }

        private static void OrderChildren(Categoria categoria)
        {
            categoria.Children = categoria.Children
                .OrderBy(c => c.Ordem)
                .ThenBy(c => c.Nome)
                .ToList();

            foreach (var child in categoria.Children)
            {
                OrderChildren(child);
            }
        }
    }
}
