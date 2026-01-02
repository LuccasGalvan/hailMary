namespace Gestao_Loja.Entities;

    public class CategoriaProduto
{

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
}

