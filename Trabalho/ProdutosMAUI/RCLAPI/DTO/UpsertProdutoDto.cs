namespace RCLAPI.DTO;

public class UpsertProdutoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    public decimal PrecoBase { get; set; }
    public int ModoDisponibilizacaoId { get; set; }

    public int Stock { get; set; }

    public List<int> CategoriaIds { get; set; } = new();

    public string? UrlImagem { get; set; }
}
