using ApiFinancas.Domain.Dtos.Input;
using ApiFinancas.Domain.Dtos.Output;

namespace ApiFinancas.Domain.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioDto> CriarUsuarioAsync(PostUsuarioDto dto);
    Task AtualizarUsuarioAsync(int id, PutUsuarioDto dto);
    Task DeletarUsuarioAsync(int id);
    Task<TokenUsuarioDto> AutenticarAsync(string email, string senha);
    Task<IEnumerable<UsuarioDto>> ObterUsuariosAsync();
}
