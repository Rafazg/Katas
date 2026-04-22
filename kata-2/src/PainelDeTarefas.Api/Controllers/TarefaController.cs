using Microsoft.AspNetCore.Mvc;
using PainelDeTarefas.Application.DTOs;
using PainelDeTarefas.Application.Services;

namespace PainelDeTarefas.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController(ITarefaService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? status)
    {
        try
        {
            var tasks = await service.ListarAsync(status);
            return Ok(tasks);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarTarefaDto dto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        try
        {
            var task = await service.CriarAsync(dto);
            return CreatedAtAction(nameof(Listar), new { id = task.Id }, task);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarTarefaDto dto)
    {
        try
        {
            var task = await service.AtualizarAsync(id, dto);
            return Ok(task);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        try
        {
            await service.RemoverAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }
}