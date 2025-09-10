using AFSilva.Core.Auth;
using AFSilva.Core.Repositories.Models;

namespace ApiFinancas.Domain.Entities;

public class Usuario : BaseModel
{
    public string NomeAcesso { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime? UltimoAcesso { get; set; }

    public TokenRequest ObterInformacoesToken()
        => new()
        {
            UserId = Id.ToString(),
            Username = NomeAcesso
        };
}
