using ApiFinancas.Domain.Entities;
using ApiFinancas.Domain.Enums;

namespace ApiFinancas.Domain.Dtos.Input;

public class PutLancamentoDto
{
    public string Descricao { get; set; } = null!;
    public Mes Mes { get; set; }
    public int Ano { get; set; }
    public decimal Valor { get; set; }
    public TipoLancamento TipoLancamento { get; set; }
    public StatusLancamento StatusLancamento { get; set; }

    public Lancamento UpdateLancamento(Lancamento lancamento)
    {
        lancamento.Descricao = Descricao;
        lancamento.Mes = Mes;
        lancamento.Ano = Ano;
        lancamento.Valor = Valor;
        lancamento.TipoLancamento = TipoLancamento;
        lancamento.StatusLancamento = StatusLancamento;

        return lancamento;
    }
}
