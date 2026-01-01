using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RESTfulAPIPWEB.Data;
using RESTfulAPIPWEB.DTO.Auth;
using RESTfulAPIPWEB.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RESTfulAPIPWEB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(
        IConfiguration config,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost("register/cliente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterCliente([FromBody] AuthRegisterRequest dto)
        => await RegisterWithRole(dto, "Cliente");

    [HttpPost("register/fornecedor")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterFornecedor([FromBody] AuthRegisterRequest dto)
        => await RegisterWithRole(dto, "Fornecedor");

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        var email = dto.Email.Trim();
        var utilizadorAtual = await _userManager.FindByEmailAsync(email);
        if (utilizadorAtual is null)
        {
            return BadRequest("Utilizador não encontrado.");
        }

        var resultado = await _signInManager.PasswordSignInAsync(email, dto.Password, false, lockoutOnFailure: false);
        if (!resultado.Succeeded)
        {
            return Unauthorized("Erro: Login inválido!");
        }

        var userRoles = await _userManager.GetRolesAsync(utilizadorAtual);
        var role = userRoles.FirstOrDefault() ?? "Cliente";

        if ((role == "Cliente" || role == "Fornecedor") && utilizadorAtual.Estado != UserEstado.Activo)
        {
            return Unauthorized("Conta pendente de ativação.");
        }

        var token = GerarToken(utilizadorAtual, role);

        return Ok(CreateAuthResponse(utilizadorAtual, role, token));
    }

    private async Task<IActionResult> RegisterWithRole(AuthRegisterRequest dto, string role)
    {
        var utilizadorExiste = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (utilizadorExiste is not null)
            return BadRequest("Já existe um utilizador com este email.");

        var novoUtilizador = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            Nome = dto.Nome,
            Apelido = dto.Apelido,
            Estado = UserEstado.Pendente
        };

        var resultado = await _userManager.CreateAsync(novoUtilizador, dto.Password);
        if (!resultado.Succeeded)
            return BadRequest("Erro ao criar o utilizador: " +
                             string.Join(", ", resultado.Errors.Select(e => e.Description)));

        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new IdentityRole(role));

        await _userManager.AddToRoleAsync(novoUtilizador, role);

        var token = GerarToken(novoUtilizador, role);

        return StatusCode(StatusCodes.Status201Created, CreateAuthResponse(novoUtilizador, role, token));
    }

    private string GerarToken(ApplicationUser utilizador, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, utilizador.Email ?? ""),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.NameIdentifier, utilizador.Id)
        };

        var token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static object CreateAuthResponse(ApplicationUser utilizador, string role, string token)
        => new
        {
            accessToken = token,
            tokenType = "bearer",
            accesstoken = token,
            tokentype = "bearer",
            utilizadorid = utilizador.Id,
            utilizadornome = utilizador.Nome,
            role
        };

    public class AuthRegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required, MinLength(6)]
        public string Password { get; set; } = default!;

        [Required, StringLength(100)]
        public string Nome { get; set; } = default!;

        [Required, StringLength(100)]
        public string Apelido { get; set; } = default!;
    }
}
