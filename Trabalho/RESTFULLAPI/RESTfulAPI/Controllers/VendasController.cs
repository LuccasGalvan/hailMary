using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Constants;
using RESTfulAPI.Data;
using RESTfulAPI.DTOs;
using RESTfulAPI.Entities;
using System.Security.Claims;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Cliente)] // só cliente autenticado compra e vê histórico
public class VendasController(AppDbContext db) : ControllerBase
{
    string UserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("UserId não encontrado no token.");

    // POST /api/Vendas
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CreateVendaRequest req)
    {
        if (req.Linhas is null || req.Linhas.Count == 0)
            return BadRequest("Tem de selecionar pelo menos um produto.");

        // agrupar produtoId (evita linhas repetidas)
        var linhasAgrupadas = req.Linhas
            .GroupBy(l => l.ProdutoId)
            .Select(g => new { ProdutoId = g.Key, Quantidade = g.Sum(x => x.Quantidade) })
            .ToList();

        if (linhasAgrupadas.Any(l => l.Quantidade <= 0))
            return BadRequest("Quantidade inválida.");

        var produtoIds = linhasAgrupadas.Select(x => x.ProdutoId).ToList();

        var produtos = await db.Produtos
            .Where(p => produtoIds.Contains(p.ProdutoId))
            .ToListAsync();

        if (produtos.Count != produtoIds.Count)
            return BadRequest("Um ou mais produtos não existem.");

        // Regras: só pode comprar produtos Ativos + que estejam para venda (IsForSale)
        var modoIds = produtos.Select(p => p.ModoDisponibilizacaoId).Distinct().ToList();
        var modos = await db.ModosDisponibilizacao
            .Where(m => modoIds.Contains(m.ModoDisponibilizacaoId))
            .ToDictionaryAsync(m => m.ModoDisponibilizacaoId);

        foreach (var p in produtos)
        {
            if (p.Estado != EstadoProduto.Ativo)
                return BadRequest($"Produto '{p.Nome}' não está ativo.");

            if (!modos.TryGetValue(p.ModoDisponibilizacaoId, out var modo) || !modo.IsForSale)
                return BadRequest($"Produto '{p.Nome}' não está disponível para venda.");

            var q = linhasAgrupadas.First(x => x.ProdutoId == p.ProdutoId).Quantidade;
            if (p.Stock < q)
                return BadRequest($"Stock insuficiente para '{p.Nome}'. Stock={p.Stock}, pedido={q}");
        }

        var venda = new Venda
        {
            ClienteId = UserId(),
            Estado = EstadoVenda.Pendente,
            DataCriacao = DateTime.Now,
            PagamentoExecutado = false
        };

        decimal total = 0m;

        foreach (var p in produtos)
        {
            var q = linhasAgrupadas.First(x => x.ProdutoId == p.ProdutoId).Quantidade;
            var precoUnit = p.PrecoFinal; // o que o cliente paga
            var totalLinha = precoUnit * q;

            venda.Linhas.Add(new LinhaVenda
            {
                ProdutoId = p.ProdutoId,
                Quantidade = q,
                PrecoUnitario = precoUnit,
                TotalLinha = totalLinha
            });

            total += totalLinha;
        }

        venda.ValorTotal = total;

        db.Vendas.Add(venda);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(Detalhe), new { id = venda.VendaId }, new { venda.VendaId, venda.ValorTotal, venda.Estado });
    }

    // GET /api/Vendas/minhas
    [HttpGet("minhas")]
    public async Task<IActionResult> Minhas()
    {
        var uid = UserId();

        var vendas = await db.Vendas.AsNoTracking()
            .Where(v => v.ClienteId == uid)
            .OrderByDescending(v => v.DataCriacao)
            .Select(v => new
            {
                v.VendaId,
                v.DataCriacao,
                v.Estado,
                v.ValorTotal,
                v.PagamentoExecutado,
                Linhas = v.Linhas.Select(l => new { l.ProdutoId, l.Quantidade, l.PrecoUnitario, l.TotalLinha })
            })
            .ToListAsync();

        return Ok(vendas);
    }

    // GET /api/Vendas/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detalhe(int id)
    {
        var uid = UserId();

        var v = await db.Vendas.AsNoTracking()
            .Where(x => x.VendaId == id && x.ClienteId == uid)
            .Select(x => new
            {
                x.VendaId,
                x.DataCriacao,
                x.Estado,
                x.ValorTotal,
                x.MetodoPagamento,
                x.Observacoes,
                x.PagamentoExecutado,
                Linhas = x.Linhas.Select(l => new { l.ProdutoId, l.Quantidade, l.PrecoUnitario, l.TotalLinha })
            })
            .FirstOrDefaultAsync();

        if (v is null) return NotFound();
        return Ok(v);
    }

    // POST /api/Vendas/{id}/pagar
    [HttpPost("{id:int}/pagar")]
    public async Task<IActionResult> Pagar(int id, [FromBody] PagarVendaRequest req)
    {
        var uid = UserId();

        var venda = await db.Vendas
            .Include(v => v.Linhas)
            .FirstOrDefaultAsync(v => v.VendaId == id && v.ClienteId == uid);

        if (venda is null) return NotFound();

        if (venda.PagamentoExecutado)
            return BadRequest("Venda já paga.");

        // atualizar stocks (simulação “no pagar”)
        var produtoIds = venda.Linhas.Select(l => l.ProdutoId).ToList();
        var produtos = await db.Produtos.Where(p => produtoIds.Contains(p.ProdutoId)).ToListAsync();

        foreach (var l in venda.Linhas)
        {
            var p = produtos.First(x => x.ProdutoId == l.ProdutoId);
            if (p.Stock < l.Quantidade)
                return BadRequest($"Stock insuficiente para '{p.Nome}'.");
            p.Stock -= l.Quantidade;
        }

        venda.PagamentoExecutado = true;
        venda.DataPagamento = DateTime.Now;
        venda.MetodoPagamento = req.MetodoPagamento;
        venda.Observacoes = req.Observacoes;

        // manter enum compatível com Gestão-Loja (confirmada/rejeitada é gestão)
        // aqui só marcamos como "Pendente" paga; confirmação fica para a Gestão-Loja.
        await db.SaveChangesAsync();

        return Ok(new { venda.VendaId, venda.PagamentoExecutado, venda.DataPagamento, venda.ValorTotal });
    }
}
