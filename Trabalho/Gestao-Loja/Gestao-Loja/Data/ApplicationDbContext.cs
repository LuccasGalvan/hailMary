using Gestao_Loja.Constants;
using Gestao_Loja.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Loja.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Produto> Produtos { get; set; }

    public DbSet<TipoCategoria> TipoCategorias{ get; set; }

    public DbSet<CategoriaProduto> CategoriaProdutos { get; set; }

    public DbSet<ModoDisponibilizacao> ModosDisponibilizacao { get; set; }

    public DbSet<Venda> Vendas { get; set; } = null!;
    public DbSet<LinhaVenda> LinhasVenda { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- RELATIONSHIP CONFIG ---

        modelBuilder.Entity<CategoriaProduto>()
            .HasKey(cp => new { cp.ProdutoId, cp.CategoriaId });

        modelBuilder.Entity<CategoriaProduto>()
            .HasOne(cp => cp.Produto)
            .WithMany(p => p.CategoriaProdutos)
            .HasForeignKey(cp => cp.ProdutoId);

        modelBuilder.Entity<CategoriaProduto>()
            .HasOne(cp => cp.Categoria)
            .WithMany(c => c.CategoriaProdutos)
            .HasForeignKey(cp => cp.CategoriaId);

        modelBuilder.Entity<TipoCategoria>().HasData(
            new TipoCategoria { TipoCategoriaId = 1, Nome = "Tipo" },
            new TipoCategoria { TipoCategoriaId = 2, Nome = "País" }
        );

        // Categorias
        modelBuilder.Entity<Categoria>().HasData(
            // Tipo
            new Categoria { CategoriaId = 1, Nome = "Coins", TipoCategoriaId = 1 },
            new Categoria { CategoriaId = 2, Nome = "Match boxes", TipoCategoriaId = 1 },
            new Categoria { CategoriaId = 3, Nome = "Stamps", TipoCategoriaId = 1 },
            new Categoria { CategoriaId = 4, Nome = "Playing Cards", TipoCategoriaId = 1 },

            // País
            new Categoria { CategoriaId = 5, Nome = "Brazil", TipoCategoriaId = 2 },
            new Categoria { CategoriaId = 6, Nome = "UK", TipoCategoriaId = 2 },
            new Categoria { CategoriaId = 7, Nome = "Japan", TipoCategoriaId = 2 }
        );

        // Modos de Disponibilização
        modelBuilder.Entity<ModoDisponibilizacao>().HasData(
            new ModoDisponibilizacao { ModoDisponibilizacaoId = 1, Nome = "Para Venda", IsForSale = true, Ativo = true },
            new ModoDisponibilizacao { ModoDisponibilizacaoId = 2, Nome = "Para Listagem", IsForSale = false, Ativo = true },
            new ModoDisponibilizacao { ModoDisponibilizacaoId = 3, Nome = "Pré-Venda", IsForSale = true, Ativo = true },
            new ModoDisponibilizacao { ModoDisponibilizacaoId = 4, Nome = "Esgotado", IsForSale = false, Ativo = true },
            new ModoDisponibilizacao { ModoDisponibilizacaoId = 5, Nome = "Interno", IsForSale = false, Ativo = false }
        );

    }

}
