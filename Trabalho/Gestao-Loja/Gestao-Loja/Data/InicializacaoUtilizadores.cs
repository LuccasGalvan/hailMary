using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Loja.Data;

public static class InicializacaoUtilizadores
{
    public static async Task CriarDadosIniciaisAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = ["Administrador", "Funcionario", "Cliente", "Fornecedor"];

        foreach (var roleName in roles)
        {
            if (!await roleManager.Roles.AnyAsync(r => r.Name == roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Utilizador admin por omissão
        var adminEmail = "admin@isec.pt";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Nome = "Administrador",
                Apelido = "Loja",
                EmailConfirmed = true,
                EstadoConta = EstadoConta.Ativo,
                TipoConta = TipoConta.Admin // não é Cliente nem Fornecedor
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Administrador");
            }
        }
    }
}
