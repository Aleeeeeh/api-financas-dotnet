using ApiFinancas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinancas.Data;

public class LancamentoContext : IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.Property(l => l.Descricao)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(l => l.Mes)
            .IsRequired();

        builder.Property(l => l.Ano)
            .IsRequired();

        builder.Property(l => l.UsuarioId)
            .IsRequired();

        builder.Property(l => l.Valor)
            .HasColumnType("decimal(7,2)")
            .IsRequired();

        builder.Property(l => l.TipoLancamento)
            .IsRequired();

        builder.Property(l => l.StatusLancamento)
            .IsRequired();
    }
}
