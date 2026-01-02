namespace Gestao_Loja.Entities;

using System.ComponentModel.DataAnnotations;



    public class LinhaVenda
    {
        public int LinhaVendaId { get; set; }

        public int VendaId { get; set; }
        public Venda Venda { get; set; } = null!;

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        // Preço por unidade no momento da venda (Precos mudam no futuro!)
        [Range(0, double.MaxValue)]
        public decimal PrecoUnitario { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalLinha { get; set; }
    }

