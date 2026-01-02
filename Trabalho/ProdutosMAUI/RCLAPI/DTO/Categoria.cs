using System.Text.Json.Serialization;

namespace RCLAPI.DTO;

public class Categoria
{
    [JsonPropertyName("categoriaId")]
    public int Id { get; set; }

    [JsonPropertyName("nome")]
    public string? Nome { get; set; }

    [JsonPropertyName("tipoCategoriaId")]
    public int TipoCategoriaId { get; set; }

    [JsonPropertyName("urlImagem")]
    public string? UrlImagem { get; set; }

    [JsonIgnore]
    public string? Imagem => UrlImagem;

}
