using AFSilva.Core.Repositories.Models;
using ApiFinancas.Domain.Dtos.Input;
using ApiFinancas.Domain.Dtos.Output;
using ApiFinancas.Domain.Entities;
using ApiFinancas.Domain.Enums;
using ApiFinancas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiFinancas.Services;

public class LancamentoService(RepositoryBase<Lancamento> repository) : ILancamentoService
{
    private readonly RepositoryBase<Lancamento> _repository = repository;

    public async Task AtualizarLancamentoAsync(int id, PutLancamentoDto dto)
    {
        Lancamento lancamento = await _repository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Lançamento não encontrado.");

        await _repository.UpdateAsync(dto.UpdateLancamento(lancamento));
    }

    public async Task AtualizarStatusLancamentoAsync(int id, int statusLancamentoId)
    {
        Lancamento lancamento = await _repository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Lançamento não encontrado.");

        ValidarDadosLancamento(lancamento);

        lancamento.StatusLancamento = (StatusLancamento)statusLancamentoId;

        await _repository.UpdateAsync(lancamento);
    }

    public async Task<LancamentoDto> CriarLancamentoAsync(PostLancamentoDto dto)
    {
        Lancamento lancamento = dto;

        ValidarDadosLancamento(lancamento);

        await _repository.CreateAsync(lancamento);

        return lancamento;
    }

    private static void ValidarDadosLancamento(Lancamento lancamento)
    {
        if (string.IsNullOrEmpty(lancamento.Descricao))
            throw new InvalidOperationException("Descrição do lançamento é obrigatória.");

        if ((int)lancamento.Mes < 1 || (int)lancamento.Mes > 12)
            throw new InvalidOperationException("Mês do lançamento inválido. Deve estar entre 1 e 12.");

        if ((int)lancamento.Ano < 1)
            throw new InvalidOperationException("Ano do lançamento inválido. Valor deve ser maior que zero");

        if (lancamento.Valor <= 0)
            throw new InvalidOperationException("Valor do lançamento deve ser maior que zero.");

        if (!Enum.IsDefined(typeof(TipoLancamento), lancamento.TipoLancamento))
            throw new InvalidOperationException("Tipo do lançamento inválido.");
    }

    public async Task DeletarLancamentoAsync(int id)
    {
        await _repository.RemoveAsync(id);
    }

    public async Task<LancamentoDto> ObterLancamentoPorIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Lançamento não encontrado.");
    }

    public async Task<IEnumerable<LancamentoDto>> ObterLancamentosPorFiltroAsync(string? descricao, int mes, int ano, TipoLancamento tipoLancamento, int usuarioId)
    {
        IQueryable<Lancamento> query = _repository.Query();

        if (usuarioId <= 0)
            throw new InvalidOperationException("ID do usuário é obrigatório.");

        query = query.Where(l => l.UsuarioInclusaoId == usuarioId);

        if (!string.IsNullOrEmpty(descricao))
            query = query.Where(l => l.Descricao.ToLower().Contains(descricao.ToLower()));

        if (mes > 0)
            query = query.Where(l => l.Mes == (Mes)mes);

        if (ano > 0)
            query = query.Where(l => l.Ano == ano);

        if (tipoLancamento > 0)
            query = query.Where(l => l.TipoLancamento == tipoLancamento);

        return await query.Select(l => new LancamentoDto(l)).ToListAsync();
    }

    public async Task<IEnumerable<LancamentoDto>> ObterLancamentosPorPeriodoAsync(int mesInicial, int anoInicial, int mesFinal, int anoFinal, int usuarioId)
    {
        PeriodoDataEhValido(mesInicial, anoInicial, mesFinal, anoFinal);

        List<Lancamento> listagemLancamentos = await _repository.Query()
            .Where(l => l.UsuarioInclusaoId == usuarioId)
            .Where(l => (int)l.Mes >= mesInicial && (int)l.Mes <= mesFinal)
            .Where(l => l.Ano >= anoInicial && l.Ano <= anoFinal)
            .ToListAsync();

        if (listagemLancamentos.Count() == 0)
            throw new InvalidOperationException("Nenhum lançamento encontrado para o período informado.");

        return listagemLancamentos.Select(l => new LancamentoDto(l)).ToList();
    }

    private static void PeriodoDataEhValido(int mesInicial, int anoInicial, int mesFinal, int anoFinal)
    {
        if (mesInicial < 1 || mesInicial > 12)
            throw new InvalidOperationException("Mês inicial inválido. Deve estar entre 1 e 12.");

        if (mesFinal < 1 || mesFinal > 12)
            throw new InvalidOperationException("Mês final inválido. Deve estar entre 1 e 12.");

        if (anoInicial < 1)
            throw new InvalidOperationException("Ano inicial inválido. Deve ser maior que 0.");

        if (anoFinal < 1)
            throw new InvalidOperationException("Ano final inválido. Deve ser maior que 0.");

        if (mesInicial > mesFinal)
            throw new InvalidOperationException("Mês inicial não pode ser maior que o mês final.");

        if (anoInicial > anoFinal)
            throw new InvalidOperationException("Ano inicial não pode ser maior que o ano final.");
    }

    public async Task<decimal> ObterSaldoPorUsuario(int usuarioId)
    {
        decimal totalReceitas = await ObterTotalReceitas(usuarioId);
        decimal totalDespesas = await ObterTotalDespesas(usuarioId);

        return totalReceitas - totalDespesas;
    }

    private async Task<decimal> ObterTotalReceitas(int usuarioId)
    {
        return await _repository.Query()
            .Where(l => l.TipoLancamento == TipoLancamento.RECEITA)
            .Where(l => l.UsuarioInclusaoId == usuarioId)
            .Where(l => l.StatusLancamento == StatusLancamento.EFETIVADO)
            .SumAsync(l => l.Valor);
    }

    private async Task<decimal> ObterTotalDespesas(int usuarioId)
    {
        return await _repository.Query()
            .Where(l => l.TipoLancamento == TipoLancamento.DESPESA)
            .Where(l => l.UsuarioInclusaoId == usuarioId)
            .Where(l => l.StatusLancamento == StatusLancamento.EFETIVADO)
            .SumAsync(l => l.Valor);
    }
}
