namespace RESTfulAPIPWEB.DTO.Vendas
{
    public class VendaLinhaDto
    {
        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }

        public decimal TotalLinha { get; set; }
    }
}
