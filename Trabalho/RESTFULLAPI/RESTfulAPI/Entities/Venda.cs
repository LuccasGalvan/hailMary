using RESTfulAPI.Constants;
using RESTfulAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.Entities;

public class Venda
{
    public int VendaId { get; set; }

    [Required]
    public string ClienteId { get; set; } = null!;

    public ApplicationUser? Cliente { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataPagamento { get; set; }
    public DateTime? DataConfirmacao { get; set; }
    public DateTime? DataExpedicao { get; set; }

    [Required]
    public EstadoVenda Estado { get; set; } = EstadoVenda.Pendente;

    [Range(0, double.MaxValue)]
    public decimal ValorTotal { get; set; }

    [StringLength(100)]
    public string? MetodoPagamento { get; set; }

    [StringLength(200)]
    public string? Observacoes { get; set; }

    public bool PagamentoExecutado { get; set; }

    public ICollection<LinhaVenda> Linhas { get; set; } = new List<LinhaVenda>();
}
