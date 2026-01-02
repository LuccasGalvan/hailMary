using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Constants;
using RESTfulAPI.Data;
using System.Security.Claims;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/fornecedor/vendas")]
[Authorize(Roles = Roles.Fornecedor)]
public class FornecedorVendasController(AppDbContext db) : ControllerBase
{
    string UserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("UserId não encontrado no token.");

    // GET /api/fornecedor/vendas
    [HttpGet]
    public async Task<IActionResult> Historico()
    {
        var uid = UserId();

        var vendas = await db.Vendas.AsNoTracking()
            .Where(v => v.Linhas.Any(l => l.Produto.FornecedorId == uid))
            .OrderByDescending(v => v.DataCriacao)
            .Select(v => new
            {
                v.VendaId,
                v.DataCriacao,
                v.Estado,
                v.ValorTotal,
                v.PagamentoExecutado,
                Linhas = v.Linhas
                    .Where(l => l.Produto.FornecedorId == uid)
                    .Select(l => new
                    {
                        l.ProdutoId,
                        Produto = l.Produto.Nome,
                        l.Quantidade,
                        l.PrecoUnitario,
                        l.TotalLinha
                    })
            })
            .ToListAsync();

        return Ok(vendas);
    }
}
