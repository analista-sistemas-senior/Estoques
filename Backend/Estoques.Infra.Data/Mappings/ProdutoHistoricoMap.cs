using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class ProdutoHistoricoMap : IEntityTypeConfiguration<ProdutoHistorico>
    {
        public void Configure(EntityTypeBuilder<ProdutoHistorico> builder)
        {
            builder.ToTable("ProdutoHistorico");
            builder.HasKey(ph => ph.IDProdutoHistorico);
            builder.Property(ph => ph.INProdutoHistoricoTipo).HasColumnType("smallint").IsRequired();
            builder.Property(ph => ph.DTProdutoHistorico).HasColumnType("date").IsRequired();
            builder.Property(ph => ph.QTProdutoHistorico).HasColumnType("numeric(18,2)").IsRequired();
            builder.Property(ph => ph.VLProdutoHistorico).HasColumnType("numeric(18,2)").IsRequired();

            builder.HasOne(ph => ph.Fornecedor).WithMany(f => f.ProdutosHistoricos).HasForeignKey(ph => ph.IDFornecedor).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ph => ph.Adquirente).WithMany(a => a.ProdutosHistoricos).HasForeignKey(ph => ph.IDAdquirente).OnDelete(DeleteBehavior.Restrict);
        }
    }
}