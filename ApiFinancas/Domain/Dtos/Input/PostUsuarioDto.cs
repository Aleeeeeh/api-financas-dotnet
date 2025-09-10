using AFSilva.Core.Auth.Password;
using ApiFinancas.Domain.Entities;

namespace ApiFinancas.Domain.Dtos.Input;

public class PostUsuarioDto
{
    public string NomeAcesso { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Email { get; set; } = null!;
    public static implicit operator Usuario(PostUsuarioDto dto) => new()
    {
        NomeAcesso = dto.NomeAcesso,
        Senha = BcryptPasswordHasher.EncriptPassword(dto.Senha),
        Email = dto.Email.ToLower()
    };
}
