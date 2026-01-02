using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTfulAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RESTfulAPI.Constants;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    public record LoginDto(string Email, string Password);

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            return Unauthorized("Credenciais inválidas.");

        var ok = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
        if (!ok.Succeeded)
            return Unauthorized("Credenciais inválidas.");

        if (user.EstadoConta != EstadoConta.Ativo)
            return Unauthorized("Conta pendente ou inativa. Aguarda ativação na Gestão-Loja.");


        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.UserName ?? user.Email ?? "")
        };

        // (opcional) roles
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return Ok(new
        {
            tokenType = "Bearer",
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            expiresIn = 3600
        });

    }

    public record RegisterClienteDto(string Email, string Password, string Nome, string? Apelido);
    public record RegisterFornecedorDto(string Email, string Password, string Nome, string? Apelido);

    [HttpPost("register/cliente")]
    public async Task<IActionResult> RegisterCliente([FromBody] RegisterClienteDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            Nome = dto.Nome,
            Apelido = dto.Apelido,
            TipoConta = TipoConta.Cliente,
            EstadoConta = EstadoConta.Pendente
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors.Select(e => e.Description));

        await _userManager.AddToRoleAsync(user, Roles.Cliente);
        return Ok("Cliente registado (pendente de ativação).");
    }

    [HttpPost("register/fornecedor")]
    public async Task<IActionResult> RegisterFornecedor([FromBody] RegisterFornecedorDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            Nome = dto.Nome,
            Apelido = dto.Apelido,
            TipoConta = TipoConta.Fornecedor,
            EstadoConta = EstadoConta.Pendente
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors.Select(e => e.Description));

        await _userManager.AddToRoleAsync(user, Roles.Fornecedor);
        return Ok("Fornecedor registado (pendente de ativação).");
    }

}
