using AFSilva.Core.Repositories.Models;
using ApiFinancas.Domain.Enums;

namespace ApiFinancas.Domain.Entities;

public class Lancamento : BaseModel
{
    public string Descricao { get; set; } = null!;
    public Mes Mes { get; set; }
    public int Ano { get; set; }
    public int UsuarioId { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public TipoLancamento TipoLancamento { get; set; }
    public StatusLancamento StatusLancamento { get; set; }
}
