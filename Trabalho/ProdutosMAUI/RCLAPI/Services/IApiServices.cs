using RCLAPI.DTO;

namespace RCLAPI.Services;

public interface IApiServices
{
    public Task<List<ProdutoDTO>> GetProdutosEspecificos(string produtoTipo, int? IdCategoria);
    public Task<(T? Data, string? ErrorMessage)> GetAsync<T>(string endpoint);
    Task<List<Categoria>> GetCategorias(int? tipoCategoriaId = null);
    Task<List<TipoCategoriaDTO>> GetTiposCategoria();
    public Task<(bool Data, string? ErrorMessage)> ActualizaFavorito(string acao,int produtoId);
    public Task<List<ProdutoDTO>> GetFavoritos();
    public Task<ApiResponse<bool>> RegistarUtilizador(Utilizador novoUtilizador);
    public Task<ApiResponse<bool>> Login(LoginModel login);
    public Task<bool> IsUserLoggedIn();
    public Task<ApiResponse<int>> CriaEncomenda(ItemCarrinhoCompra itemCarrinhoCompra);
    public Task<ApiResponse<bool>> PagaEncomenda(int encomendaId);
    public Task<List<EncomendaDTO>> GetEncomendasByUser();
    public Task<string?> GetUserId();


    Task<bool> IsFornecedor();
    Task<List<FornecedorProdutoDto>> GetMeusProdutosFornecedor();
    Task<ApiResponse<int>> CriarProdutoFornecedor(UpsertProdutoDto dto);
    Task<ApiResponse<bool>> EditarProdutoFornecedor(int id, UpsertProdutoDto dto);
    Task<ApiResponse<bool>> ApagarProdutoFornecedor(int id);


    public Task Logout();
}
