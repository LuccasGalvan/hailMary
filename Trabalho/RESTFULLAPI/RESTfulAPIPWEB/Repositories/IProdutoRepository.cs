using RESTfulAPIPWEB.Entity;

namespace RESTfulAPIPWEB.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterProdutosAsync(int? categoriaId, string? texto, int? modoDisponibilizacaoId);
        Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId);
        Task<IEnumerable<Produto>> ObterProdutosPromocaoAsync();
        Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync();
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
        Task<Produto?> ObterDetalheProdutoAsync(int id);
        Task<Produto?> ObterProdutoEmDestaqueAsync();
    }
}
