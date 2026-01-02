using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.Data;

public enum TipoConta { Cliente, Fornecedor, Admin, Funcionario }
public enum EstadoConta { Pendente, Ativo, Inativo }

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Apelido { get; set; }

    public TipoConta? TipoConta { get; set; }
    public EstadoConta EstadoConta { get; set; } = EstadoConta.Pendente;

}
