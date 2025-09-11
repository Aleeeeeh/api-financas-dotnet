using AFSilva.Core.Auth.Password;
using ApiFinancas.Domain.Entities;

namespace ApiFinancas.Domain.Dtos.Input;

public class PutUsuarioDto
{
    public string NomeAcesso { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public Usuario Update(Usuario usuario)
    {
        usuario.NomeAcesso = NomeAcesso;
        usuario.Senha = BcryptPasswordHasher.EncriptPassword(Senha);

        return usuario;
    }
}
