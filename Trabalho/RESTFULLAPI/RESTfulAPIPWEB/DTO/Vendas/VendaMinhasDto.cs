using RESTfulAPIPWEB.Entity.Enums;

namespace RESTfulAPIPWEB.DTO.Vendas
{
    public class VendaMinhasDto
    {
        public int VendaId { get; set; }

        public DateTime DataCriacao { get; set; }

        public EncomendaEstado Estado { get; set; }

        public decimal ValorTotal { get; set; }

        public bool PagamentoExecutado { get; set; }

        public List<VendaLinhaDto> Linhas { get; set; } = new();
    }
}
