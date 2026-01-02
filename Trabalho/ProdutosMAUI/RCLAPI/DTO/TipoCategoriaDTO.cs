using System.Text.Json.Serialization;

namespace RCLAPI.DTO;

public class TipoCategoriaDTO
{
    [JsonPropertyName("tipoCategoriaId")]
    public int TipoCategoriaId { get; set; }

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = string.Empty;
}
