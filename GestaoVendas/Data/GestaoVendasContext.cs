using GestaoVendas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoVendas.Data
{
    public class GestaoVendasContext : IdentityDbContext
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

        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<AcessoTipoUsuario> AcessoTipoUsuario { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuario { get; set; }

        public DbSet<IdentityUser> Usuario { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemVenda>()
                .HasKey(x => new { x.ProdutoId, x.VendaId });

            builder.Entity<ProdutoEstoque>()
                .HasKey(x => new { x.EstoqueId, x.ProdutoId });
        }


        public DbSet<GestaoVendas.Models.Funcionalidade> Funcionalidade { get; set; }
    }
}
