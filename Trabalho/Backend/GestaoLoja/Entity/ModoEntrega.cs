using GestaoLoja.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestaoLoja.Entity
{
    public class ModoEntrega
    {
        public int Id   { get; set; }

        [StringLength(StringLength.NomeMaxLength)]
        [Required]
        public string? Nome { get; set; }

        [StringLength(StringLength.DescricaoMaxLength)]
        public string? Detalhe { get; set; }

        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
