using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfulAPIPWEB.Entity
{
    public class EncomendaItem
    {
        [Key]
        public int LinhaVendaId { get; set; }

        [Required]
        public int VendaId { get; set; }
        public Encomenda? Encomenda { get; set; }

        [Required]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        // Freeze price at purchase time
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoUnitario { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalLinha { get; set; }
    }
}
