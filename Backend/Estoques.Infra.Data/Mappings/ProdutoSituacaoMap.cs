using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class ProdutoSituacaoMap : IEntityTypeConfiguration<ProdutoSituacao>
    {
        public void Configure(EntityTypeBuilder<ProdutoSituacao> builder)
        {
            builder.ToTable("ProdutoSituacao");
            builder.HasKey(ps => ps.IDProdutoSituacao);
            builder.Property(ps => ps.NMProdutoSituacao).HasMaxLength(255).IsRequired();
        }
    }
}