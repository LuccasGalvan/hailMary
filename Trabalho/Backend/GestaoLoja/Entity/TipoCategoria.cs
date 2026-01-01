using System.ComponentModel.DataAnnotations;

namespace GestaoLoja.Entity
{
    public class TipoCategoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = default!;
    }
}
