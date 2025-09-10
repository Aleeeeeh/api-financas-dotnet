namespace ApiFinancas.Domain.Dtos.Output;

public class TokenUsuarioDto
{
    public int UsuarioId { get; set; }
    public string NomeAcesso { get; set; } = null!;
    public string Token { get; set; } = null!;
}
