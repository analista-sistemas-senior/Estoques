using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class ProdutoTipoMap : IEntityTypeConfiguration<ProdutoTipo>
    {
        public void Configure(EntityTypeBuilder<ProdutoTipo> builder)
        {
            builder.ToTable("ProdutoTipo");
            builder.HasKey(pt => pt.IDProdutoTipo);
            builder.Property(pt => pt.NMProdutoTipo).HasMaxLength(255).IsRequired();
        }
    }
}