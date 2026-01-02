using System.Security.Claims;
using Gestao_Loja.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Loja.Data.Services;

public class UserManagementService
{
    private readonly UserManager<ApplicationUser> _userManager;

    // Constantes de roles
    private const string RoleAdmin = "Administrador";
    private const string RoleFuncionario = "Funcionario";
    // Se tiveres roles Cliente/Fornecedor no Identity, vamos deixar de os usar.

    public UserManagementService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // Helpers ----------------------

    private static bool IsAdmin(ClaimsPrincipal user) =>
        user.IsInRole(RoleAdmin);

    private static bool IsAdminOrFuncionario(ClaimsPrincipal user) =>
        user.IsInRole(RoleAdmin) || user.IsInRole(RoleFuncionario);

    private async Task<ApplicationUser?> GetUserAsync(string userId) =>
        await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

    /// <summary>
    /// Garante que o utilizador tem no máximo um role Identity.
    /// Remove todos os roles atuais e, se newRole != null, atribui apenas esse.
    /// </summary>
    private async Task SetSingleRoleAsync(ApplicationUser user, string? newRole)
    {
        var currentRoles = await _userManager.GetRolesAsync(user);

        if (currentRoles.Any())
        {
            await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
        }

        if (!string.IsNullOrWhiteSpace(newRole))
        {
            await _userManager.AddToRoleAsync(user, newRole);
        }
    }

    // 1) Gerir Funcionário (só Admin) -----------------------------

    public async Task ToggleFuncionarioRoleAsync(string userId, ClaimsPrincipal currentUser)
    {
        if (!IsAdmin(currentUser))
            throw new UnauthorizedAccessException("Apenas Administrador pode gerir Funcionários.");

        var user = await GetUserAsync(userId);
        if (user is null) return;

        // Se já é Funcionário -> retirar role (ficar sem role especial)
        if (await _userManager.IsInRoleAsync(user, RoleFuncionario))
        {
            // Retira o role, deixa o utilizador como "normal"
            await SetSingleRoleAsync(user, null);
        }
        else
        {
            // Se é Administrador, não queremos torná-lo Funcionário
            if (await _userManager.IsInRoleAsync(user, RoleAdmin))
                throw new InvalidOperationException("Não é possível alterar um Administrador para Funcionário.");

            // Passa a ser só Funcionário
            await SetSingleRoleAsync(user, RoleFuncionario);

            // Opcional mas recomendável: Funcionário não deve ser Cliente/Fornecedor
            user.TipoConta = null;
            user.EstadoConta = EstadoConta.Pendente;
            await _userManager.UpdateAsync(user);
        }
    }

    // 2) Definir TipoConta Cliente/Fornecedor (Admin ou Func) -----

    public async Task DefinirTipoContaAsync(string userId, TipoConta? novoTipo, ClaimsPrincipal currentUser)
    {
        if (!IsAdminOrFuncionario(currentUser))
            throw new UnauthorizedAccessException("Sem permissões para gerir o tipo de conta.");

        var user = await GetUserAsync(userId);
        if (user is null) return;

        // Não queremos gerir aqui TipoConta Admin/Funcionario
        if (novoTipo is TipoConta.Admin or TipoConta.Funcionario)
            throw new InvalidOperationException("TipoConta Admin/Funcionario não deve ser gerido aqui.");

        // Se o utilizador é Admin ou Funcionário (role), não pode ser Cliente/Fornecedor
        bool isUserAdmin = await _userManager.IsInRoleAsync(user, RoleAdmin);
        bool isUserFuncionario = await _userManager.IsInRoleAsync(user, RoleFuncionario);
        if (isUserAdmin || isUserFuncionario)
        {
            // Poderias também simplesmente ignorar, mas lançar exceção torna o erro evidente
            throw new InvalidOperationException("Administradores e Funcionários não podem ter tipo de conta Cliente/Fornecedor.");
        }

        if (novoTipo is null)
        {
            // Remover tipo de conta de negócio
            user.TipoConta = null;
            user.EstadoConta = EstadoConta.Inativo;
        }
        else
        {
            // Atribui Cliente ou Fornecedor
            user.TipoConta = novoTipo.Value;

            // Novo tipo começa como Pendente se estiver Inativo
            if (user.EstadoConta == EstadoConta.Inativo)
                user.EstadoConta = EstadoConta.Pendente;
        }

        // IMPORTANTE: deixamos de mexer em roles Cliente/Fornecedor.
        await _userManager.UpdateAsync(user);
    }

    // 3) Alterar EstadoConta Pendente <-> Ativo (Admin ou Func) -----

    /// <summary>
    /// Alterna EstadoConta entre Pendente e Ativo, apenas para Cliente/Fornecedor.
    /// Apenas Admin ou Funcionário podem fazer isto.
    /// </summary>
    public async Task ToggleEstadoContaAsync(string userId, ClaimsPrincipal currentUser)
    {
        if (!IsAdminOrFuncionario(currentUser))
            throw new UnauthorizedAccessException("Sem permissões para alterar o estado da conta.");

        var user = await GetUserAsync(userId);
        if (user is null) return;

        bool isTargetAdmin = await _userManager.IsInRoleAsync(user, RoleAdmin);
        bool isTargetFuncionario = await _userManager.IsInRoleAsync(user, RoleFuncionario);

        // Nunca mexer no estado de Administrador
        if (isTargetAdmin)
            return;

        // --- Funcionário: só o ADMIN pode alterar o EstadoConta ---
        if (isTargetFuncionario)
        {
            // Se o utilizador autenticado não é Admin, não pode mudar o estado do Funcionário
            if (!IsAdmin(currentUser))
                return;
        }
        else
        {
            // --- Não é Funcionário: só faz sentido se for Cliente ou Fornecedor ---
            if (user.TipoConta is not TipoConta.Cliente and not TipoConta.Fornecedor)
                return;
        }

        // A partir daqui, temos permissão para mudar o estado:
        if (user.EstadoConta == EstadoConta.Pendente)
            user.EstadoConta = EstadoConta.Ativo;
        else if (user.EstadoConta == EstadoConta.Ativo)
            user.EstadoConta = EstadoConta.Pendente;
        // se estiver Inativo, continua Inativo; poderias decidir outro comportamento

        await _userManager.UpdateAsync(user);
    }

}
