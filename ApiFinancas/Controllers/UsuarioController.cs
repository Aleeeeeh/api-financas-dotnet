using ApiFaturamento.Domain.Dtos.Shared;
using ApiFinancas.Domain.Dtos.Input;
using ApiFinancas.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinancas.Controllers;

[ApiController]
[Route("usuario")]
[Authorize]
public class UsuarioController(IUsuarioService service) : ControllerBase
{
    private readonly IUsuarioService _service = service;

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CriarUsuarioAsync([FromBody] PostUsuarioDto dto)
    {
        try
        {
            return Ok(await _service.CriarUsuarioAsync(dto));
        }
        catch (ArgumentException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarUsuarioAsync([FromRoute] int id, [FromBody] PutUsuarioDto dto)
    {
        try
        {
            await _service.AtualizarUsuarioAsync(id, dto);

            return Ok("Usuário atualizado com sucesso.");
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
    public async Task<IActionResult> DeletarUsuarioAsync([FromRoute] int id)
    {
        try
        {
            await _service.DeletarUsuarioAsync(id);

            return Ok("Usuário deletado com sucesso.");
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

    [HttpPost("autentica")]
    [AllowAnonymous]
    public async Task<IActionResult> AutenticarAsync([FromQuery] string email, [FromQuery] string senha)
    {
        try
        {
            return Ok(await _service.AutenticarAsync(email, senha));
        }
        catch (ArgumentException ex)
        {
            return NotFound(new ResponseErrorDto(ex));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorDto(ex));
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObterTodosUsuariosAsync()
    {
        try
        {
            return Ok(await _service.ObterUsuariosAsync());
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
