namespace RESTfulAPI.DTOs;

public class CreateVendaRequest
{
    public List<CreateVendaLinha> Linhas { get; set; } = new();
}

public class CreateVendaLinha
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}
