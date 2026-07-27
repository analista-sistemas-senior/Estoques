using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class ProdutoFabricanteMap : IEntityTypeConfiguration<ProdutoFabricante>
    {
        public void Configure(EntityTypeBuilder<ProdutoFabricante> builder)
        {
            builder.ToTable("ProdutoFabricante");
            builder.HasKey(pf => pf.IDProdutoFabricante);
            builder.Property(pf => pf.NMProdutoFabricante).HasMaxLength(255).IsRequired();
        }
    }
}