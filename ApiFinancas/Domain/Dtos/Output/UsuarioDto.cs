using ApiFinancas.Domain.Entities;

namespace ApiFinancas.Domain.Dtos.Output;

public class UsuarioDto
{
    public int Id { get; set; }
    public string? NomeAcesso { get; set; }
    public string? Email { get; set; }
    public DateTime? UltimoAcesso { get; set; }
    public static implicit operator UsuarioDto(Usuario usuario) => new()
    {
        Id = usuario.Id,
        NomeAcesso = usuario.NomeAcesso,
        Email = usuario.Email,
        UltimoAcesso = usuario.UltimoAcesso
    };
}
