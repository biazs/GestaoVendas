using GestaoVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoVendas.Data
{
    public class GestaoVendasContext : DbContext
    {
        public GestaoVendasContext(DbContextOptions<GestaoVendasContext> options)
            : base(options)
        {
        }

        public DbSet<Fornecedor> Fornecedor { get; set; }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<ProdutoEstoque> ProdutoEstoque { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemVenda>()
                .HasKey(x => new { x.ProdutoId, x.VendaId });

            modelBuilder.Entity<ProdutoEstoque>()
                .HasKey(x => new { x.EstoqueId, x.ProdutoId });
        }
    }
}
