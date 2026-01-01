using System.ComponentModel.DataAnnotations;

namespace RESTfulAPIPWEB.Entity
{
    public class TipoCategoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = default!;
    }
}
