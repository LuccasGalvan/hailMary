using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestaoLoja.Data;
using GestaoLoja.Entity.Enums;

namespace GestaoLoja.Entity
{
    public class Encomenda
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendaId { get; set; }

        // Identity user id (Cliente)
        [Required]
        public string ClienteId { get; set; } = default!;

        public ApplicationUser? Cliente { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataConfirmacao { get; set; }
        public DateTime? DataExpedicao { get; set; }

        public EncomendaEstado Estado { get; set; } = EncomendaEstado.PendentePagamento;

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal { get; set; }

        [StringLength(100)]
        public string? MetodoPagamento { get; set; }

        [StringLength(200)]
        public string? Observacoes { get; set; }

        public bool PagamentoExecutado { get; set; }

        // Navigation
        public ICollection<EncomendaItem> Itens { get; set; } = new List<EncomendaItem>();
    }
}
