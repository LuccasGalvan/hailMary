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
    new TipoCategoria { TipoCategoriaId = 2, Nome = "Editora" },
    new TipoCategoria { TipoCategoriaId = 3, Nome = "Grupo" },
    new TipoCategoria { TipoCategoriaId = 4, Nome = "Género" },
    new TipoCategoria { TipoCategoriaId = 5, Nome = "Plataforma" }
);

        // Categorias
        modelBuilder.Entity<Categoria>().HasData(
            // Tipo
            new Categoria { CategoriaId = 1, Nome = "Filme", TipoCategoriaId = 1 },
            new Categoria { CategoriaId = 2, Nome = "Música", TipoCategoriaId = 1 },
            new Categoria { CategoriaId = 3, Nome = "Jogo", TipoCategoriaId = 1 },
            new Categoria { CategoriaId = 4, Nome = "Série", TipoCategoriaId = 1 },

            // Editora
            new Categoria { CategoriaId = 5, Nome = "Sony", TipoCategoriaId = 2 },
            new Categoria { CategoriaId = 6, Nome = "Warner", TipoCategoriaId = 2 },
            new Categoria { CategoriaId = 7, Nome = "Universal", TipoCategoriaId = 2 },
            new Categoria { CategoriaId = 8, Nome = "EA Games", TipoCategoriaId = 2 },

            // Grupo / Artista
            new Categoria { CategoriaId = 9, Nome = "Metallica", TipoCategoriaId = 3 },
            new Categoria { CategoriaId = 10, Nome = "Queen", TipoCategoriaId = 3 },
            new Categoria { CategoriaId = 11, Nome = "Eminem", TipoCategoriaId = 3 },
            new Categoria { CategoriaId = 12, Nome = "Coldplay", TipoCategoriaId = 3 },

            // Género
            new Categoria { CategoriaId = 13, Nome = "Ação", TipoCategoriaId = 4 },
            new Categoria { CategoriaId = 14, Nome = "Drama", TipoCategoriaId = 4 },
            new Categoria { CategoriaId = 15, Nome = "Rock", TipoCategoriaId = 4 },
            new Categoria { CategoriaId = 16, Nome = "Pop", TipoCategoriaId = 4 },
            new Categoria { CategoriaId = 17, Nome = "RAP", TipoCategoriaId = 4 },

            // Plataforma
            new Categoria { CategoriaId = 18, Nome = "PS5", TipoCategoriaId = 5 },
            new Categoria { CategoriaId = 19, Nome = "Xbox", TipoCategoriaId = 5 },
            new Categoria { CategoriaId = 20, Nome = "PC", TipoCategoriaId = 5 },
            new Categoria { CategoriaId = 21, Nome = "Nintendo Switch", TipoCategoriaId = 5 }
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
