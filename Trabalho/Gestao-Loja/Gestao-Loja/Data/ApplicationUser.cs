using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Gestao_Loja.Constants;

namespace Gestao_Loja.Data;


// Add profile data for application users by adding properties to the ApplicationUser class

public enum TipoConta
{
    Cliente,
    Fornecedor,
    Admin,
    Funcionario
}

public enum EstadoConta
{
    Pendente,
    Ativo,
    Inativo
}
public class ApplicationUser : IdentityUser
{

    [Required]
    [StringLength(StringLength.NomeMaxLength)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(StringLength.NomeMaxLength)]
    public string? Apelido { get; set; }
    public TipoConta? TipoConta { get; set; }
    public EstadoConta EstadoConta { get; set; } = EstadoConta.Pendente;
}
