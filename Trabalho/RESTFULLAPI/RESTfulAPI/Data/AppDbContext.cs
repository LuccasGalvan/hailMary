using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; } = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;
    public DbSet<TipoCategoria> TipoCategorias { get; set; } = null!;
    public DbSet<CategoriaProduto> CategoriaProdutos { get; set; } = null!;
    public DbSet<ModoDisponibilizacao> ModosDisponibilizacao { get; set; } = null!;
    public DbSet<Venda> Vendas { get; set; } = null!;
    public DbSet<LinhaVenda> LinhasVenda { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
    }
}
