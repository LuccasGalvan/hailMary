using Gestao_Loja.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Gestao_Loja.Security;

public class EmpresaUserRequirement : IAuthorizationRequirement { }

public class EmpresaUserHandler : AuthorizationHandler<EmpresaUserRequirement>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public EmpresaUserHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        EmpresaUserRequirement requirement)
    {
        // Must be logged in
        if (context.User?.Identity?.IsAuthenticated != true)
            return;

        // 1) Administrador => always allowed
        if (context.User.IsInRole("Administrador"))
        {
            context.Succeed(requirement);
            return;
        }

        // 2) Funcionário => only if EstadoConta == Ativo
        if (context.User.IsInRole("Funcionario"))
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user != null && user.EstadoConta == EstadoConta.Ativo)
            {
                context.Succeed(requirement);
            }
            return;
        }

        // 3) Any other user (sem role, Cliente, Fornecedor, etc.) => no access
        // (do nothing => requirement not satisfied)
    }
}
