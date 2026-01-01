using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using RCLAPI.DTO;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.Entity.Core.Objects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.NetworkInformation;

using System.Security.Claims;

namespace RCLAPI.Services;
public class ApiService : IApiServices
{
    private static Token _token;
    private readonly ILogger<ApiService> _logger;
    private readonly HttpClient _httpClient = new();

    private class VendaMinhasDto
    {
        public int VendaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Estado { get; set; }
        public decimal ValorTotal { get; set; }
        public bool PagamentoExecutado { get; set; }
        public List<LinhaDto> Linhas { get; set; } = new();

        public class LinhaDto
        {
            public int ProdutoId { get; set; }
            public int Quantidade { get; set; }
            public decimal PrecoUnitario { get; set; }
            public decimal TotalLinha { get; set; }
        }
    }



    JsonSerializerOptions _serializerOptions;

    private List<ProdutoDTO> produtos;
    private ProdutoDTO produto;

    private List<Categoria> categorias;

    private ProdutoDTO _detalhesProduto;
        public ApiService(ILogger<ApiService> logger)
        {
        

        _logger = logger;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _detalhesProduto = new ProdutoDTO();
        categorias = new List<Categoria>();
    }

    public void SetToken(Token token)
    {
        _token = token;
    }

    private void AddAuthorizationHeader()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (_token is not null && !string.IsNullOrEmpty(_token.accesstoken))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token.accesstoken);
        }
    }

    private static string Base64UrlDecodeToString(string input)
    {
        input = input.Replace('-', '+').Replace('_', '/');
        switch (input.Length % 4)
        {
            case 2: input += "=="; break;
            case 3: input += "="; break;
        }
        var bytes = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(bytes);
    }

    private string? GetRoleFromJwt(string jwt)
    {
        try
        {
            var parts = jwt.Split('.');
            if (parts.Length < 2) return null;

            var payloadJson = Base64UrlDecodeToString(parts[1]);
            using var doc = JsonDocument.Parse(payloadJson);
            var root = doc.RootElement;

            // pode vir como "role" ou como claim URI
            if (root.TryGetProperty("role", out var roleProp))
            {
                if (roleProp.ValueKind == JsonValueKind.String) return roleProp.GetString();
                if (roleProp.ValueKind == JsonValueKind.Array && roleProp.GetArrayLength() > 0)
                    return roleProp[0].GetString();
            }

            var uriRole = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            if (root.TryGetProperty(uriRole, out var uriRoleProp))
            {
                if (uriRoleProp.ValueKind == JsonValueKind.String) return uriRoleProp.GetString();
                if (uriRoleProp.ValueKind == JsonValueKind.Array && uriRoleProp.GetArrayLength() > 0)
                    return uriRoleProp[0].GetString();
            }

            return null;
        }
        catch { return null; }
    }

    // ********************* Fornecedor  **********

    public Task<bool> IsFornecedor()
    {
        if (_token?.accesstoken is null) return Task.FromResult(false);

        var role = GetRoleFromJwt(_token.accesstoken);
        return Task.FromResult(string.Equals(role, "Fornecedor", StringComparison.OrdinalIgnoreCase));
    }


    public async Task<List<FornecedorProdutoDto>> GetMeusProdutosFornecedor()
    {
        AddAuthorizationHeader();

        var res = await _httpClient.GetAsync($"{AppConfig.BaseUrl}api/fornecedor/produtos");
        if (!res.IsSuccessStatusCode) return new List<FornecedorProdutoDto>();

        var json = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<FornecedorProdutoDto>>(json, _serializerOptions) ?? new();
    }

    public async Task<ApiResponse<int>> CriarProdutoFornecedor(UpsertProdutoDto dto)
    {
        try
        {
            var json = JsonSerializer.Serialize(dto, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await PostRequest("api/fornecedor/produtos", content);
            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadAsStringAsync();
                return new ApiResponse<int> { Data = 0, ErrorMessage = err };
            }

            // API devolve { produtoId, estado }
            var body = await res.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(body);
            var id = doc.RootElement.GetProperty("produtoId").GetInt32();

            return new ApiResponse<int> { Data = id };
        }
        catch (Exception ex)
        {
            return new ApiResponse<int> { Data = 0, ErrorMessage = ex.Message };
        }
    }

    public async Task<ApiResponse<bool>> EditarProdutoFornecedor(int id, UpsertProdutoDto dto)
    {
        try
        {
            AddAuthorizationHeader();

            var json = JsonSerializer.Serialize(dto, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _httpClient.PutAsync($"{AppConfig.BaseUrl}api/fornecedor/produtos/{id}", content);
            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadAsStringAsync();
                return new ApiResponse<bool> { Data = false, ErrorMessage = err };
            }

            return new ApiResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool> { Data = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<ApiResponse<bool>> ApagarProdutoFornecedor(int id)
    {
        try
        {
            AddAuthorizationHeader();

            var res = await _httpClient.DeleteAsync($"{AppConfig.BaseUrl}api/fornecedor/produtos/{id}");
            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadAsStringAsync();
                return new ApiResponse<bool> { Data = false, ErrorMessage = err };
            }

            return new ApiResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool> { Data = false, ErrorMessage = ex.Message };
        }
    }




    // ********************* Categorias  **********
    public async Task<List<Categoria>> GetCategorias(int? tipoCategoriaId = null)
    {
        string endpoint = tipoCategoriaId.HasValue
            ? $"api/Categorias?tipoCategoriaId={tipoCategoriaId.Value}"
            : "api/Categorias";

        try
        {
            var res = await _httpClient.GetAsync($"{AppConfig.BaseUrl}{endpoint}");
            if (!res.IsSuccessStatusCode) return new();

            var content = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Categoria>>(content, _serializerOptions) ?? new();
        }
        catch
        {
            return new();
        }
    }


    // ********************* Produtos  **********
    public async Task<List<ProdutoDTO>> GetProdutosEspecificos(string produtoTipo,int? IdCategoria)
    {
        // para já: se vier categoria, filtra por categoria; senão lista todos ativos
        string endpoint = IdCategoria != null
            ? $"api/Produtos?categoriaId={IdCategoria}&soAtivos=true"
            : $"api/Produtos?soAtivos=true";

        try
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{AppConfig.BaseUrl}{endpoint}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                produtos = JsonSerializer.Deserialize<List<ProdutoDTO>>(content, _serializerOptions) ?? new List<ProdutoDTO>();
            }
            else
            {
                return new List<ProdutoDTO>();
            }
        }
        catch
        {
            return new List<ProdutoDTO>();
        }

        return produtos;
    }

    public async Task<ProdutoDTO> GetProdutoById(int produtoId)
    {
        try
        {
            // Construir o endpoint para buscar o produto pelo ID
            var endpoint = $"api/Produtos/{produtoId}";

            // Chamar o método GetAsync para buscar o produto
            HttpResponseMessage httpResponseMessage =
                await _httpClient.GetAsync($"{AppConfig.BaseUrl}{endpoint}");

            // Verificar se houve erro na requisição
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string content = "";
                content = await httpResponseMessage.Content.ReadAsStringAsync();
                produto = JsonSerializer.Deserialize<ProdutoDTO>(content, _serializerOptions)!;  
                
            }

            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        return produto;
    }

    public async Task<(T?Data, string?ErrorMessage)>GetAsync<T>(string endpoint)
    {
        try
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync(AppConfig.BaseUrl + endpoint);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<T>(responseString, _serializerOptions);
                return (data ?? Activator.CreateInstance<T>(), null);
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    string errorMessage = "Unauthorized";
                    _logger.LogWarning(errorMessage);
                    return (default, errorMessage);
                }
                string generalErrorMessage = $"Erro na requisição: {response.ReasonPhrase}";
                _logger.LogError(generalErrorMessage);
                return (default, generalErrorMessage);
            }
        }
        catch (HttpRequestException ex)
        {
            string errrMessage = $"Erro de requisição HTTP: {ex.Message}";
            _logger.LogError(errrMessage);
            return (default, errrMessage);
        }
        catch (JsonException ex)
        {
            string errorMessage = $"Erro de desserialização JSON: {ex.Message}";
            _logger.LogError(ex.Message);
            return (default, errorMessage);
        }
        catch (Exception ex)
        {
            string errorMessage = $"Erro inesperado: {ex.Message}";
            _logger.LogError(ex.Message);
            return (default, errorMessage);
        }
    }

    // ***************** Compras ******************
    public async Task<ApiResponse<int>> CriaEncomenda(ItemCarrinhoCompra itemCarrinhoCompra)
    {
        try
        {
            AddAuthorizationHeader();

            var body = new
            {
                linhas = new[]
                {
                new { produtoId = itemCarrinhoCompra.ProdutoId, quantidade = itemCarrinhoCompra.Quantidade }
            }
            };

            var json = JsonSerializer.Serialize(body, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostRequest("api/Vendas", content);
            var respBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return new ApiResponse<int> { Data = 0, ErrorMessage = respBody };

            // ✅ extrair vendaId do JSON
            try
            {
                using var doc = JsonDocument.Parse(respBody);
                if (doc.RootElement.TryGetProperty("vendaId", out var vendaIdProp))
                {
                    return new ApiResponse<int> { Data = vendaIdProp.GetInt32() };
                }

                if (doc.RootElement.TryGetProperty("vendaId", out var v1))
                    return new ApiResponse<int> { Data = v1.GetInt32() };

                if (doc.RootElement.TryGetProperty("id", out var v2))
                    return new ApiResponse<int> { Data = v2.GetInt32() };

                if (doc.RootElement.TryGetProperty("vendaID", out var v3))
                    return new ApiResponse<int> { Data = v3.GetInt32() };
            }
            catch { /* ignora e cai no erro abaixo */ }

            return new ApiResponse<int>
            {
                Data = 0,
                ErrorMessage = "A API não devolveu vendaId no response do POST /api/Vendas."
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<int> { Data = 0, ErrorMessage = ex.Message };
        }
    }




    public async Task<List<EncomendaDTO>> GetEncomendasByUser()
    {
        string endpoint = $"api/Vendas/minhas";

        try
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{AppConfig.BaseUrl}{endpoint}");

            if (!response.IsSuccessStatusCode)
                return new List<EncomendaDTO>();

            var responseContent = await response.Content.ReadAsStringAsync();
            var vendas = JsonSerializer.Deserialize<List<VendaMinhasDto>>(responseContent, _serializerOptions)
                        ?? new List<VendaMinhasDto>();

            // converter para o teu EncomendaDTO antigo
            var encomendas = vendas.Select(v => new EncomendaDTO
            {
                Id = v.VendaId,                      // <-- aqui é Id
                ProdutoId = v.Linhas.FirstOrDefault()?.ProdutoId ?? 0,  // compatibilidade (UI antiga)
                Preco = v.ValorTotal,
                Quantidade = v.Linhas.Sum(l => l.Quantidade),
                Enviada = false,                     // gestão-loja trata expedição/estado
                Estado = v.Estado.ToString(),
                DataCriacao = v.DataCriacao,
                DataFinalizacao = null,
                EmStock = true,                      // na tua API só cria se houver stock
                Paga = v.PagamentoExecutado
            }).ToList();


            return encomendas;
        }
        catch
        {
            return new List<EncomendaDTO>();
        }
    }


    public async Task<ApiResponse<bool>> PagaEncomenda(int encomendaId)
    {
        string endpoint = $"api/Vendas/{encomendaId}/pagar";

        try
        {

            AddAuthorizationHeader();
            
            var content = new StringContent(
                JsonSerializer.Serialize(new { metodoPagamento = "Simulado", observacoes = "" }, _serializerOptions),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{AppConfig.BaseUrl}{endpoint}", content);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<bool> { Data = true };
            }
            else
            {

                var err = await response.Content.ReadAsStringAsync();
                return new ApiResponse<bool>
                {
                    Data = false,
                    ErrorMessage = string.IsNullOrWhiteSpace(err)
                        ? $"{(int)response.StatusCode} {response.ReasonPhrase}"
                        : err
                };
            }
        }
        catch(Exception ex)
        {
            _logger.LogError($"Erro ao pagar a encomenda: {ex.Message}");
            return new ApiResponse<bool> { Data = false, ErrorMessage = $"Erro ao pagar a encomenda: {ex.Message}" };
        }
    }


    // ****************** Utilizadores ********************
    public async Task<ApiResponse<bool>> RegistarUtilizador(Utilizador novoUtilizador)
    {
        try
        {
            // escolher endpoint conforme tipo
            var endpoint = (novoUtilizador.Tipo?.Equals("Fornecedor", StringComparison.OrdinalIgnoreCase) == true)
                ? "api/Auth/register/fornecedor"
                : "api/Auth/register/cliente";

            // payload MINIMO (só o que a tua API espera)
            var payload = new
            {
                email = novoUtilizador.Email,
                password = novoUtilizador.Password,
                nome = novoUtilizador.Nome,
                apelido = novoUtilizador.Apelido
            };

            var json = JsonSerializer.Serialize(payload, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostRequest(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return new ApiResponse<bool> { Data = false, ErrorMessage = err };
            }

            return new ApiResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool> { Data = false, ErrorMessage = ex.Message };
        }
    }

    public Task<bool> IsUserLoggedIn()
    => Task.FromResult(_token is not null && !string.IsNullOrWhiteSpace(_token.accesstoken));




    public async Task<ApiResponse<bool>> Login(LoginModel login)
{
    try
    {
        string endpoint = "api/Auth/login"; // <-- tua API

        var json = JsonSerializer.Serialize(login, _serializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await PostRequest(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Login falhou: {(int)response.StatusCode} - {err}");
            return new ApiResponse<bool> { Data = false, ErrorMessage = err };
        }

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TokenResponse>(jsonResult, _serializerOptions);

        if (result?.AccessToken is null)
        {
            return new ApiResponse<bool> { Data = false, ErrorMessage = "Token inválido na resposta do servidor." };
        }

        // Guardar token no formato que o teu ApiService usa internamente
        _token = new Token
        {
            accesstoken = result.AccessToken,
            tokentype = result.TokenType ?? "Bearer",
        };

        // meter header Bearer para os próximos pedidos
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", result.AccessToken);

        return new ApiResponse<bool> { Data = true };
    }
    catch (Exception ex)
    {
        string errorMessage = $"Erro inesperado no login: {ex.Message}";
        _logger.LogError(ex, errorMessage);
        return new ApiResponse<bool> { Data = false, ErrorMessage = errorMessage };
    }
}

    private async Task<HttpResponseMessage> PostRequest(string enderecoURL, HttpContent content)
    {
        try
        {
            AddAuthorizationHeader();
            var result = await _httpClient.PostAsync($"{AppConfig.BaseUrl}{enderecoURL}", content);
            return result;
        }
        catch (Exception ex)
        {
            // Log o erro ou trata conforme necessario
            _logger.LogError($"Erro ao enviar requisição POST para enderecoURL: {ex.Message}");
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }



    // *************** Gerir Favoritos ******************
// Sem backend real de favoritos: para não partir UI/Slider, devolvemos produtos ativos como "favoritos".
// Assim o slider continua a ter items.

public async Task<List<ProdutoDTO>> GetFavoritos()
{
    try
    {
        // Reutiliza o endpoint real (produtos ativos)
        string endpoint = "api/Produtos?soAtivos=true";
        var response = await _httpClient.GetAsync($"{AppConfig.BaseUrl}{endpoint}");

        if (!response.IsSuccessStatusCode)
            return new List<ProdutoDTO>();

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<ProdutoDTO>>(json, _serializerOptions)
                   ?? new List<ProdutoDTO>();

        return data;
    }
    catch
    {
        return new List<ProdutoDTO>();
    }
}

public Task<(bool Data, string? ErrorMessage)> ActualizaFavorito(string acao, int produtoId)
{
    // "No-op": diz que correu bem para o UI não partir
    return Task.FromResult((true, (string?)null));
}

    public string? GetUserRole()
    {
        if (_token?.accesstoken is null) return null;

        try
        {
            var parts = _token.accesstoken.Split('.');
            if (parts.Length != 3) return null;

            string payload = parts[1]
                .Replace('-', '+')
                .Replace('_', '/');

            switch (payload.Length % 4)
            {
                case 2: payload += "=="; break;
                case 3: payload += "="; break;
            }

            var jsonBytes = Convert.FromBase64String(payload);
            var json = Encoding.UTF8.GetString(jsonBytes);

            using var doc = JsonDocument.Parse(json);

            // role claim pode vir como string ou array
            if (doc.RootElement.TryGetProperty("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", out var roleProp))
            {
                if (roleProp.ValueKind == JsonValueKind.String) return roleProp.GetString();
                if (roleProp.ValueKind == JsonValueKind.Array && roleProp.GetArrayLength() > 0) return roleProp[0].GetString();
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public Task<string?> GetUserId()
    {
        var token = _token?.accesstoken;
        if (string.IsNullOrWhiteSpace(token)) return Task.FromResult<string?>(null);

        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3) return Task.FromResult<string?>(null);

            var payload = parts[1].Replace('-', '+').Replace('_', '/');
            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');

            var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
            using var doc = JsonDocument.Parse(json);

            // comuns: "nameid" ou o uri do NameIdentifier
            if (doc.RootElement.TryGetProperty("nameid", out var v1))
                return Task.FromResult<string?>(v1.GetString());

            const string nameIdUri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            if (doc.RootElement.TryGetProperty(nameIdUri, out var v2))
                return Task.FromResult<string?>(v2.GetString());

            return Task.FromResult<string?>(null);
        }
        catch
        {
            return Task.FromResult<string?>(null);
        }
    }

    public async Task<List<TipoCategoriaDTO>> GetTiposCategoria()
    {
        string endpoint = "api/TipoCategorias";
        try
        {
            var res = await _httpClient.GetAsync($"{AppConfig.BaseUrl}{endpoint}");
            if (!res.IsSuccessStatusCode) return new();

            var content = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TipoCategoriaDTO>>(content, _serializerOptions) ?? new();
        }
        catch
        {
            return new();
        }
    }


    public Task Logout()
    {
        _token = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        return Task.CompletedTask;
    }
}
