namespace RESTfulAPI.DTOs;

public class PagarVendaRequest
{
    public string MetodoPagamento { get; set; } = "Simulado";
    public string? Observacoes { get; set; }
}
