using System.Text.Json.Serialization;

namespace RESTfulAPIPWEB.DTO;

public class FavoritoDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("produtoId")]
    public int ProdutoId { get; set; }

    [JsonPropertyName("produto")]
    public ProdutoDto? Produto { get; set; }

    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
}
