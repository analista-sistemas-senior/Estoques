using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Context
{
    public class EstoquesDbContext(DbContextOptions<EstoquesDbContext> options) : DbContext(options)
    {
        public DbSet<Adquirente> Adquirente { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<ProdutoFabricante> ProdutoFabricante { get; set; }
        public DbSet<ProdutoHistorico> ProdutoHistorico { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoSituacao> ProdutoSituacao { get; set; }
        public DbSet<ProdutoTipo> ProdutoTipo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ProdutoMedida> ProdutoMedida { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstoquesDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}