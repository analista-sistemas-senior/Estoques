using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(p => p.IDProduto);
            builder.Property(p => p.NMProduto).HasMaxLength(255).IsRequired();
            builder.Property(p => p.DSProduto).HasMaxLength(1024).IsRequired();
            builder.Property(p => p.INProdutoCor).HasColumnType("smallint").IsRequired();
            builder.Property(p => p.QTProduto).HasColumnType("numeric(18,2)").IsRequired();
            builder.Property(p => p.INProdutoMedida).HasColumnType("smallint").IsRequired(false);
            builder.Property(p => p.LKProdutoImagem).HasMaxLength(1024).IsRequired(false);

            builder.HasOne(p => p.ProdutoTipo).WithMany(pt => pt.Produtos).HasForeignKey(p => p.IDProdutoTipo).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.ProdutoSituacao).WithMany(ps => ps.Produtos).HasForeignKey(p => p.IDProdutoSituacao).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.ProdutoFabricante).WithMany(pf => pf.Produtos).HasForeignKey(p => p.IDProdutoFabricante).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.ProdutosHistoricos).WithOne(ph => ph.Produto).HasForeignKey(ph => ph.IDProduto).OnDelete(DeleteBehavior.Cascade);
        }
    }
}