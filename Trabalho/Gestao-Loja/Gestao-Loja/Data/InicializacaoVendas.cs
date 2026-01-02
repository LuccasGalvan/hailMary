using Gestao_Loja.Entities;
using Gestao_Loja.Constants;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Loja.Data;

public static class InicializacaoVendas
{
    public static async Task CriarVendasAsync(ApplicationDbContext db)
    {
        // If vendas already exist, do nothing
        if (await db.Vendas.AnyAsync())
            return;

        // Need some products first
        var produtos = await db.Produtos.Take(5).ToListAsync();
        if (!produtos.Any())
            return;

        // Need at least 1 user
        var cliente = await db.Users.FirstOrDefaultAsync();
        if (cliente is null)
            return;

        // Create 3 vendas
        List<Venda> vendas = new();

        for (int i = 1; i <= 3; i++)
        {
            var venda = new Venda
            {
                ClienteId = cliente.Id,
                DataCriacao = DateTime.Now.AddDays(-i),
                Estado = EstadoVenda.Pendente,
            };

            // Each venda has 1 line for testing
            var produto = produtos[i % produtos.Count];

            var linha = new LinhaVenda
            {
                ProdutoId = produto.ProdutoId,
                Quantidade = 1,
                PrecoUnitario = produto.PrecoFinal,
                TotalLinha = produto.PrecoFinal
            };

            venda.Linhas.Add(linha);
            venda.ValorTotal = linha.TotalLinha;

            vendas.Add(venda);
        }

        await db.Vendas.AddRangeAsync(vendas);
        await db.SaveChangesAsync();
    }
}
