using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class ProdutoMedidaMap : IEntityTypeConfiguration<ProdutoMedida>
    {
        public void Configure(EntityTypeBuilder<ProdutoMedida> builder)
        {
            builder.ToTable("ProdutoMedida");
            builder.HasKey(pf => pf.IDProdutoMedida);
            builder.Property(pf => pf.MDProdutoMedida).HasMaxLength(255).IsRequired();
        }
    }
}