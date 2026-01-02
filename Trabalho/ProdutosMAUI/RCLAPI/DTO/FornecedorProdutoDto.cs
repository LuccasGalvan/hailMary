using System.Text.Json.Serialization;

namespace RCLAPI.DTO;

public class FornecedorProdutoDto
{
    [JsonPropertyName("produtoId")]
    public int ProdutoId { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("descricao")]
    public string Descricao { get; set; } = string.Empty;

    [JsonPropertyName("precoBase")]
    public decimal PrecoBase { get; set; }

    [JsonPropertyName("precoFinal")]
    public decimal PrecoFinal { get; set; }

    [JsonPropertyName("percentagemComissao")]
    public decimal PercentagemComissao { get; set; }

    [JsonPropertyName("estado")]
    public int Estado { get; set; }


    [JsonPropertyName("modoDisponibilizacaoId")]
    public int ModoDisponibilizacaoId { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("urlImagem")]
    public string? UrlImagem { get; set; }

    [JsonPropertyName("categoriaIds")]
    public List<int> CategoriaIds { get; set; } = new();
}
