using System.ComponentModel.DataAnnotations;
using RESTfulAPI.Constants;

namespace RESTfulAPI.Entities;

public class TipoCategoria
{
    public int TipoCategoriaId { get; set; }

    [StringLength(StringLength.NomeMaxLength)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}
