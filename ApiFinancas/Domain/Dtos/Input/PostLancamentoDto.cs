using ApiFinancas.Domain.Entities;
using ApiFinancas.Domain.Enums;

namespace ApiFinancas.Domain.Dtos.Input;

public class PostLancamentoDto
{
    public string Descricao { get; set; } = null!;
    public Mes Mes { get; set; }
    public int Ano { get; set; }
    public decimal Valor { get; set; }
    public TipoLancamento TipoLancamento { get; set; }
    public DateTime DataLancamento { get; set; }

    public static implicit operator Lancamento(PostLancamentoDto dto) =>
        new()
        {
            Descricao = dto.Descricao,
            Mes = dto.Mes,
            Ano = dto.Ano,
            Valor = dto.Valor,
            TipoLancamento = dto.TipoLancamento,
            DataLancamento = dto.DataLancamento
        };
}
