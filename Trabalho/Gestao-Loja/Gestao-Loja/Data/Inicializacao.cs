using Gestao_Loja.Constants;
using Gestao_Loja.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Gestao_Loja.Data;

public record SeedProdutoDto(
    string Nome,
    string Descricao,
    decimal PrecoBase,
    decimal PercentagemComissao,
    int Stock,
    string Estado,
    int ModoDisponibilizacaoId,
    List<int> Categorias,
    string? ImagePath
);
public static class InicializacaoProdutos
{
    public static async Task CriarProdutosAsync(ApplicationDbContext db, string jsonPath)
    {
        if (await db.Produtos.AnyAsync())
            return;

        if (!File.Exists(jsonPath))
            throw new FileNotFoundException($"Seed file not found: {jsonPath}");

        var json = await File.ReadAllTextAsync(jsonPath);
        var items = JsonSerializer.Deserialize<List<SeedProdutoDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

        // Validate reference data exists
        var modosIds = await db.ModosDisponibilizacao.Select(m => m.ModoDisponibilizacaoId).ToListAsync();
        var catIds = await db.Categorias.Select(c => c.CategoriaId).ToListAsync();

        foreach (var dto in items)
        {
            if (!modosIds.Contains(dto.ModoDisponibilizacaoId))
                throw new Exception($"Invalid ModoDisponibilizacaoId: {dto.ModoDisponibilizacaoId} for {dto.Nome}");

            foreach (var cid in dto.Categorias)
                if (!catIds.Contains(cid))
                    throw new Exception($"Invalid CategoriaId: {cid} for {dto.Nome}");
        }

        var produtos = new List<Produto>();

        foreach (var dto in items)
        {
            if (!Enum.TryParse<EstadoProduto>(dto.Estado, true, out var estadoParsed))
                estadoParsed = EstadoProduto.Pendente;

            var precoFinal = dto.PrecoBase + (dto.PrecoBase * (dto.PercentagemComissao / 100m));

            produtos.Add(new Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                PrecoBase = dto.PrecoBase,
                PercentagemComissao = dto.PercentagemComissao,
                PrecoFinal = precoFinal,
                Stock = dto.Stock,
                Estado = estadoParsed,
                ModoDisponibilizacaoId = dto.ModoDisponibilizacaoId,
                UrlImagem = string.IsNullOrWhiteSpace(dto.ImagePath) ? "/img/no-image.png" : dto.ImagePath
            });
        }

        await db.Produtos.AddRangeAsync(produtos);
        await db.SaveChangesAsync();

        // Build category links
        for (int i = 0; i < produtos.Count; i++)
        {
            var produto = produtos[i];
            var dto = items[i];

            foreach (var catId in dto.Categorias.Distinct())
            {
                await db.CategoriaProdutos.AddAsync(new CategoriaProduto
                {
                    ProdutoId = produto.ProdutoId,
                    CategoriaId = catId
                });
            }
        }

        await db.SaveChangesAsync();
    }
}
