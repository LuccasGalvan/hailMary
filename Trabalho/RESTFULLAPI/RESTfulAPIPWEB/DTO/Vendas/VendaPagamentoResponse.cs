using RESTfulAPIPWEB.Entity.Enums;

namespace RESTfulAPIPWEB.DTO.Vendas
{
    public class VendaPagamentoResponse
    {
        public int VendaId { get; set; }

        public EncomendaEstado Estado { get; set; }

        public DateTime PagoEmUtc { get; set; }
    }
}
