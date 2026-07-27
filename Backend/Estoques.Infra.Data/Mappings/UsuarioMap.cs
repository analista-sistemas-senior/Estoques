using Estoques.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoques.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.IDUsuario);
            builder.Property(u => u.NMUsuario).HasMaxLength(255).IsRequired();
            builder.Property(u => u.NMLogin).HasMaxLength(255).IsRequired();
            builder.Property(u => u.CDSenha).HasMaxLength(512).IsRequired();

            builder.HasMany(u => u.ProdutosFabricantes).WithOne(pf => pf.Usuario).HasForeignKey(pf => pf.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.ProdutosSituacoes).WithOne(ps => ps.Usuario).HasForeignKey(ps => ps.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Produtos).WithOne(p => p.Usuario).HasForeignKey(p => p.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.ProdutosTipos).WithOne(pt => pt.Usuario).HasForeignKey(pt => pt.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Fornecedores).WithOne(f => f.Usuario).HasForeignKey(f => f.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Adquirentes).WithOne(a => a.Usuario).HasForeignKey(a => a.IDUsuario).OnDelete(DeleteBehavior.Cascade);
        }
    }
}