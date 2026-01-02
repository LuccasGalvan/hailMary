namespace Gestao_Loja.Entities;

public class ModoDisponibilizacao
{
    public int ModoDisponibilizacaoId { get; set; }
    public string Nome { get; set; } = string.Empty;   // ex: "Para Venda", "Para Listagem"
    public bool IsForSale { get; set; }                // true = vende, false = só lista
    public bool Ativo { get; set; } = true;

    public ICollection<Produto> Produtos { get; set; } = new List<Produto>(); ///Ajudar nas consultas da bd
}