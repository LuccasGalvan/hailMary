using RESTfulAPIPWEB.Data;
using RESTfulAPIPWEB.Entity.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RESTfulAPIPWEB.Entity
{
    public class Produto
    {
        [Key]
        [Column("Id")]
        public int ProdutoId { get; set; }

        [NotMapped]
        public int Id
        {
            get => ProdutoId;
            set => ProdutoId = value;
        }

        [StringLength(100)]
        [Required]
        public string? Nome { get; set; }

        [StringLength(100)]
        [Required]
        [StringLength(100)]
        [Required]
        public string? Descricao { get; set; }
        public string? UrlImagem {  get; set; }
        public byte[]?Imagem{get;set;}

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoBase { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? PercentagemComissao { get; set; } // e.g. 15.00 for 15%

        [Column(TypeName = "decimal(10,2)")]
        public decimal? PrecoFinal { get; set; }

        public ProdutoEstado Estado { get; set; } = ProdutoEstado.Pendente;

        // Ownership: supplier can only manage "their" products.
        [Required]
        public string FornecedorId { get; set; } = default!;
        [JsonIgnore]
        public ApplicationUser? Fornecedor { get; set; }

        public bool Promocao { get; set; }
        public bool MaisVendido { get; set; }

        [Range(0, int.MaxValue)]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public bool ParaVenda { get; set; } = true;
        public string? Origem {  get; set; }
        public int CategoriaId  { get; set; }
        public Categoria? categoria { get; set; }
        public ICollection<Categoria> CategoriaProdutos { get; set; } = new List<Categoria>();

        [JsonIgnore]
        public int? ModoEntregaId { get; set; }
        public ModoEntrega? modoentrega { get; set; }

        public int? ModoDisponibilizacaoId { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
       
    }
}
