using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWEB.Constants;

namespace RESTfulAPIPWEB.Data;

public static class InicializacaoUtilizadores
{
    public static async Task CriarDadosIniciaisAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = [Roles.Administrador, Roles.Funcionario, Roles.Cliente, Roles.Fornecedor];

        foreach (var roleName in roles)
        {
            if (!await roleManager.Roles.AnyAsync(r => r.Name == roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminEmail = "admin@gestaoloja.local";
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
                TipoConta = TipoConta.Admin
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, Roles.Administrador);
            }
        }
    }
}
