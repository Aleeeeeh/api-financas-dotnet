using ApiFaturamento.Domain.Dtos.Shared;
using ApiFinancas.Domain.Dtos.Input;
using ApiFinancas.Domain.Enums;
using ApiFinancas.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinancas.Controllers;

[ApiController]
[Route("lancamento")]
[Authorize]
public class LancamentoController(ILancamentoService service) : ControllerBase
{
    private readonly ILancamentoService _service = service;

    [HttpPost]
    public async Task<IActionResult> CriarLancamentoAsync([FromBody] PostLancamentoDto dto)
    {
        try
        {
            return Ok(await _service.CriarLancamentoAsync(dto));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarLancamentoAsync([FromRoute] int id, [FromBody] PutLancamentoDto dto)
    {
        try
        {
            await _service.AtualizarLancamentoAsync(id, dto);

            return Ok("Lancamento atualizado com sucesso.");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpPut("atualiza-status-lancamento/{id}")]
    public async Task<IActionResult> AtualizarStatusLancamentoAsync([FromRoute] int id, [FromQuery] int statusLancamentoId)
    {
        try
        {
            await _service.AtualizarStatusLancamentoAsync(id, statusLancamentoId);

            return Ok("Status do lançamento atualizado com sucesso.");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarLancamentoAsync([FromRoute] int id)
    {
        try
        {
            await _service.DeletarLancamentoAsync(id);

            return Ok("Lançamento deletado com sucesso.");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterLancamentoPorIdAsync([FromRoute] int id)
    {
        try
        {
            return Ok(await _service.ObterLancamentoPorIdAsync(id));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpGet("por-perido")]
    public async Task<IActionResult> ObterLancamentosPorPeriodoAsync([FromQuery] int mesInicial, [FromQuery] int anoInicial, [FromQuery] int mesFinal, [FromQuery] int anoFinal, [FromQuery] int usuarioId)
    {
        try
        {
            return Ok(await _service.ObterLancamentosPorPeriodoAsync(mesInicial, anoInicial, mesFinal, anoFinal, usuarioId));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpGet("por-filtro")]
    public async Task<IActionResult> ObterLancamentosPorFiltroAsync([FromQuery] string? descricao, [FromQuery] int mes, [FromQuery] int ano, [FromQuery] int tipoLancamento, [FromQuery] int usuarioId)
    {
        try
        {
            return Ok(await _service.ObterLancamentosPorFiltroAsync(descricao, mes, ano, (TipoLancamento)tipoLancamento, usuarioId));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }
}
