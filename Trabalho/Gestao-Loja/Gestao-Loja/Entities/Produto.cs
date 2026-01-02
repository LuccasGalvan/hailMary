using Gestao_Loja.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_Loja.Entities;

    public class Produto
{
    public int ProdutoId { get; set; }
    public string? FornecedorId { get; set; }

    [StringLength(StringLength.NomeMaxLength)]
    [Required]
    public string Nome { get; set; }

    [StringLength(StringLength.DescricaoMaxLength)]
    [Required]
    public string Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoBase { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoFinal { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal PercentagemComissao { get; set; }
    public EstadoProduto Estado { get; set; } = EstadoProduto.Pendente;
    public int ModoDisponibilizacaoId { get; set; }
    public ModoDisponibilizacao ModoDisponibilizacao { get; set; }
    public int Stock { get; set; }
    public ICollection<CategoriaProduto> CategoriaProdutos { get; set; } = new List<CategoriaProduto>();
    public string? UrlImagem { get; set; }
    public byte[]? Imagem { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}

