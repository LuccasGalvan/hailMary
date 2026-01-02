using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCLAPI.DTO;

public class Utilizador
{
    public string? Nome { get; set; }
    public string? Apelido { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Endereço de Email Inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A indicação da Password é obrigatória!")]
    public string Password { get; set; }

    //[Required(ErrorMessage = "A confirmação da Password é obrigatória!")]
    //[Compare("Password", ErrorMessage = "A Password e a Confirmação da Password não coincidem")]
    //public string ConfirmPassword { get; set; }


    [ValidarNIF(ErrorMessage = "NIF inválido!")]
    public long? NIF { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public byte[]? Fotografia { get; set; }
    public string? UrlImagem { get; set; }

    public string Tipo { get; set; } = "Cliente"; // Cliente | Fornecedor

    // Validação customizada para o NIF
    public class ValidarNIF: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // Inserir o código que está no site das Finanças
            if (value is long nif)
            {
                return nif > 100;
            }
            return false;
        }
    }
}


//// Validação customizada para o NIF
//public class ValidarNIF : ValidationAttribute
//{
//    public override bool IsValid(object value)
//    {
//        if (value is not long nif) return false;

//        string nifStr = nif.ToString();

//        // Verifica se o NIF tem exatamente 9 dígitos
//        if (nifStr.Length != 9) return false;

//        // Verifica o primeiro dígito
//        int primeiroDigito = int.Parse(nifStr[0].ToString());
//        if (!(primeiroDigito == 1 || primeiroDigito == 2 || primeiroDigito == 3 ||
//              primeiroDigito == 5 || primeiroDigito == 6 || primeiroDigito == 7 ||
//              primeiroDigito == 8 || primeiroDigito == 9))
//        {
//            return false;
//        }

//        // Calcula o dígito de controlo
//        int soma = 0;
//        for (int i = 0; i < 8; i++)
//        {
//            soma += (nifStr[i] - '0') * (9 - i);
//        }

//        int resto = soma % 11;
//        int digitoControlo = (resto == 0 || resto == 1) ? 0 : 11 - resto;

//        // Verifica se o último dígito coincide
//        return digitoControlo == (nifStr[8] - '0');
//    }