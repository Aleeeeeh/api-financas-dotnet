using ApiFinancas.Domain.Dtos.Input;
using ApiFinancas.Domain.Dtos.Output;
using ApiFinancas.Domain.Enums;

namespace ApiFinancas.Domain.Interfaces;

public interface ILancamentoService
{
    Task<LancamentoDto> CriarLancamentoAsync(PostLancamentoDto dto);
    Task AtualizarLancamentoAsync(int id, PutLancamentoDto dto);
    Task AtualizarStatusLancamentoAsync(int id, int statusLancamentoId);
    Task DeletarLancamentoAsync(int id);
    Task<LancamentoDto> ObterLancamentoPorIdAsync(int id);
    Task<IEnumerable<LancamentoDto>> ObterLancamentosPorPeriodoAsync(int mesInicial, int anoInicial, int mesFinal, int anoFinal, int usuarioId);
    Task<IEnumerable<LancamentoDto>> ObterLancamentosPorFiltroAsync(string? descricao, int mes, int ano, TipoLancamento tipoLancamento, int usuarioId);
    Task<decimal> ObterSaldoPorUsuario(int usuarioId);
}
