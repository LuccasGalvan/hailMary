using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RESTfulAPI.Constants;

namespace RESTfulAPI.Entities;

public class Categoria
{
    public int CategoriaId { get; set; }

    [StringLength(StringLength.DescricaoMaxLength)]
    public string? Nome { get; set; }

    public string? UrlImagem { get; set; }
    public byte[]? Imagem { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public int TipoCategoriaId { get; set; }
    public TipoCategoria TipoCategoria { get; set; } = null!;

    public ICollection<CategoriaProduto> CategoriaProdutos { get; set; } = new List<CategoriaProduto>();
}
