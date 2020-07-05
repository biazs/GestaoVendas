using GestaoVendas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoVendas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<ProdutoEstoque> ProdutoEstoque { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemVenda>()
                .HasKey(x => new { x.ProdutoId, x.VendaId });

            builder.Entity<ProdutoEstoque>()
                .HasKey(x => new { x.EstoqueId, x.ProdutoId });
        }

    }
}
