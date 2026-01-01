using System.Text.Json.Serialization;

namespace RCLAPI.DTO;

public class TokenResponse
{
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("tokenType")]
    public string? TokenType { get; set; }

}
