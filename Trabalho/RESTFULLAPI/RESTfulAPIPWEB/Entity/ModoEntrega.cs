using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RESTfulAPIPWEB.Entity
{
    public class ModoEntrega
    {
        public int Id   { get; set; }

        [StringLength(RESTfulAPIPWEB.Constants.StringLength.NomeMaxLength)]
        [Required]
        public string? Nome { get; set; }

        [StringLength(RESTfulAPIPWEB.Constants.StringLength.DescricaoMaxLength)]
        public string? Detalhe { get; set; }

        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
