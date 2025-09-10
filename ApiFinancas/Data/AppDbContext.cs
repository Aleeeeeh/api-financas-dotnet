using ApiFinancas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiFinancas.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Financas");

        modelBuilder.ApplyConfiguration(new UsuarioContext());
    }
}
