using RESTfulAPI.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfulAPI.Entities;

public class Produto
{
    public int ProdutoId { get; set; }
    public string? FornecedorId { get; set; }

    [StringLength(StringLength.NomeMaxLength)]
    [Required]
    public string Nome { get; set; } = string.Empty;

    [StringLength(StringLength.DescricaoMaxLength)]
    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoBase { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoFinal { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal PercentagemComissao { get; set; }

    public EstadoProduto Estado { get; set; } = EstadoProduto.Pendente;

    public int ModoDisponibilizacaoId { get; set; }
    public ModoDisponibilizacao ModoDisponibilizacao { get; set; } = null!;

    public int Stock { get; set; }

    public ICollection<CategoriaProduto> CategoriaProdutos { get; set; } = new List<CategoriaProduto>();

    public string? UrlImagem { get; set; }
    public byte[]? Imagem { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
