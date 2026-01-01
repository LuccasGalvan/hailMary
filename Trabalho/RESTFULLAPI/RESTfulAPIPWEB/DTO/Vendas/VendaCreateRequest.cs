namespace RESTfulAPIPWEB.DTO.Vendas
{
    public class VendaCreateRequest
    {
        public List<VendaLinhaRequest> Linhas { get; set; } = new();
    }
}
