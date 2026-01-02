using Microsoft.AspNetCore.Identity;
using GestaoLoja.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoLoja.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Apelido { get; set; }

        public TipoConta? TipoConta { get; set; }
        public EstadoConta EstadoConta { get; set; } = EstadoConta.Pendente;
        public long? NIF { get; set; }
        public string? Rua { get; set; }
        public string ? Localidade1 {  get; set; }
        public string? Localidade2 { get; set; }
        public string? Pais { get; set; }
        public byte[]? Fotografia { get; set; }

        public UserEstado Estado { get; set; } = UserEstado.Pendente;
    }

}
