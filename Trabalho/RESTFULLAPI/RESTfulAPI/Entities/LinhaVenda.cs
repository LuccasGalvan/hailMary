using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.Entities;

public class LinhaVenda
{
    public int LinhaVendaId { get; set; }

    public int VendaId { get; set; }
    public Venda Venda { get; set; } = null!;

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }

    [Range(0, double.MaxValue)]
    public decimal PrecoUnitario { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TotalLinha { get; set; }
}
