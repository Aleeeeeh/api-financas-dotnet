using ApiFinancas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinancas.Data;

public class UsuarioContext : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasQueryFilter(l => !l.EstaExcluido);

        builder.Property(u => u.NomeAcesso)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Senha)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.UltimoAcesso)
            .IsRequired(false);
    }
}
