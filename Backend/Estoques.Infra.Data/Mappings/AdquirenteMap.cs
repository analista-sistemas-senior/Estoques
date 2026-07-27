using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class AdquirenteMap : IEntityTypeConfiguration<Adquirente>
    {
        public void Configure(EntityTypeBuilder<Adquirente> builder)
        {
            builder.ToTable("Adquirente");
            builder.HasKey(a => a.IDAdquirente);
            builder.Property(a => a.NMAdquirente).HasMaxLength(255).IsRequired();
            builder.Property(a => a.TXEndereco).HasMaxLength(255).IsRequired(false);
            builder.Property(a => a.TXAnotacao).HasMaxLength(1024).IsRequired(false);
        }
    }
}