using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");
            builder.HasKey(f => f.IDFornecedor);
            builder.Property(f => f.NMFornecedor).HasMaxLength(255).IsRequired();
            builder.Property(f => f.TXEndereco).HasMaxLength(255).IsRequired(false);
            builder.Property(f => f.TXAnotacao).HasMaxLength(1024).IsRequired(false);
        }
    }
}