using Gestao_Loja.Constants;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Loja.Entities;

public class TipoCategoria
{
    public int TipoCategoriaId { get; set; }

    [StringLength(StringLength.NomeMaxLength)]
    public string Nome { get; set; } //Tipo, Editora, Grupo, Genero, etc
    public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}
