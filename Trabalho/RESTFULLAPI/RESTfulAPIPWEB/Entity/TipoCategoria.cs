using System.ComponentModel.DataAnnotations;

namespace RESTfulAPIPWEB.Entity
{
    public class TipoCategoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(RESTfulAPIPWEB.Constants.StringLength.NomeMaxLength)]
        public string Nome { get; set; } = default!;

        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
    }
}
