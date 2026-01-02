namespace RESTfulAPIPWEB.DTO.Vendas
{
    public class CreateVendaRequest
    {
        public List<VendaLinhaRequest> Linhas { get; set; } = new();
    }
}
