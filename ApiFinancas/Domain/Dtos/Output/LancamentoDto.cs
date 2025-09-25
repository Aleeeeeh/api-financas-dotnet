using ApiFinancas.Domain.Entities;
using ApiFinancas.Domain.Enums;

namespace ApiFinancas.Domain.Dtos.Output;

public class LancamentoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = null!;
    public Mes Mes { get; set; }
    public int Ano { get; set; }
    public int UsuarioId { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public TipoLancamento TipoLancamento { get; set; }
    public StatusLancamento StatusLancamento { get; set; }

    public static implicit operator LancamentoDto(Lancamento lancamento) =>
        new()
        {
            Id = lancamento.Id,
            Descricao = lancamento.Descricao,
            Mes = lancamento.Mes,
            Ano = lancamento.Ano,
            UsuarioId = lancamento.UsuarioInclusaoId,
            Valor = lancamento.Valor,
            Data = lancamento.DataLancamento,
            TipoLancamento = lancamento.TipoLancamento,
            StatusLancamento = lancamento.StatusLancamento
        };

    public LancamentoDto() { }

    public LancamentoDto(Lancamento lancamento)
    {
        Id = lancamento.Id;
        Descricao = lancamento.Descricao;
        Mes = lancamento.Mes;
        Ano = lancamento.Ano;
        UsuarioId = lancamento.UsuarioInclusaoId;
        Valor = lancamento.Valor;
        Data = lancamento.DataLancamento;
        TipoLancamento = lancamento.TipoLancamento;
        StatusLancamento = lancamento.StatusLancamento;
    }
}
