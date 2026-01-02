using Microsoft.AspNetCore.Identity;
using RESTfulAPIPWEB.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RESTfulAPIPWEB.Data
{
    public enum TipoConta { Cliente, Fornecedor, Admin, Funcionario }
    public enum EstadoConta { Pendente, Ativo, Inativo }

    public class ApplicationUser : IdentityUser
    {
        public UserEstado Estado { get; set; } = UserEstado.Pendente;

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Apelido { get; set; }

        public TipoConta? TipoConta { get; set; }
        public EstadoConta EstadoConta { get; set; } = EstadoConta.Pendente;
        public long? NIF { get; set; }
        public string? Rua { get; set; }
        public string? Localidade1 { get; set; }
        public string? Localidade2 { get; set; }
        public string? Pais { get; set; }
        public byte[]? Fotografia { get; set; }
    }
}
