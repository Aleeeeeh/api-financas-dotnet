namespace ApiFaturamento.Domain.Dtos.Shared;

public class ResponseErrorDto
{
    public string? Mensagem { get; set; }
    public string? ExcecaoInterna { get; set; }
    public string? Detalhes { get; set; }

    public ResponseErrorDto(HttpRequestException ex)
    {
        Mensagem = ex.Message;
        ExcecaoInterna = ex.InnerException?.Message;
        Detalhes = ex.StackTrace?.Trim();
    }

    public ResponseErrorDto(ArgumentException ex)
    {
        Mensagem = ex.Message;
        ExcecaoInterna = ex.InnerException?.Message;
        Detalhes = ex.StackTrace?.Trim();
    }

    public ResponseErrorDto(InvalidOperationException ex)
    {
        Mensagem = ex.Message;
        ExcecaoInterna = ex.InnerException?.Message;
        Detalhes = ex.StackTrace?.Trim();
    }

    public ResponseErrorDto(Exception ex)
    {
        Mensagem = ex.Message;
        ExcecaoInterna = ex.InnerException?.Message;
        Detalhes = ex.StackTrace?.Trim();
    }
}
