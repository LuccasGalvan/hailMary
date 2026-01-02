using Microsoft.AspNetCore.Http;
using RESTfulAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RCLAPI.DTO;

public class ProdutoDTO
{
    [JsonPropertyName("produtoId")]
    public int Id { get; set; }
    [StringLength(100)]
    [Required]
    public string? Nome { get; set; }
    [StringLength(200)]
    [Required]
    public string? Descricao { get; set; }

    [StringLength(200)]
    
    public string? Origem { get; set; }

    [StringLength(200)]
    [Required]
    [JsonPropertyName("urlImagem")]
    public string? UrlImagem { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [JsonPropertyName("precoFinal")]
    public decimal Preco { get; set; }

    [JsonPropertyName("stock")]
    public decimal EmStock { get; set; }
    [JsonIgnore]
    public bool Disponivel => EmStock > 0;

    public byte[]? Imagem { get; set; }


    [JsonIgnore]
    [NotMapped]
    public bool Favorito { get; set; }
}
