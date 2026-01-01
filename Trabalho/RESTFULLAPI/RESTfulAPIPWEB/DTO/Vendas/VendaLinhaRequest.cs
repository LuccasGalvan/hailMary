using System.ComponentModel.DataAnnotations;

namespace RESTfulAPIPWEB.DTO.Vendas
{
    public class VendaLinhaRequest
    {
        [Required]
        public int ProdutoId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }
    }
}
