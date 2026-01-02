using GestaoLoja.Constants;
using System.ComponentModel.DataAnnotations;

namespace GestaoLoja.Entity
{
    public class TipoCategoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(StringLength.NomeMaxLength)]
        public string Nome { get; set; } = default!;
    }
}
