using Microsoft.AspNetCore.Mvc;
using SistemaReservas.Api.Models;
using SistemaReservas.Api.Services;

namespace SistemaReservas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendamentosController : ControllerBase
{
    private readonly AgendamentosService _agendamentosService;

    public AgendamentosController(AgendamentosService agendamentosService)
    {
        _agendamentosService = agendamentosService;
    }

    [HttpGet]
    public async Task<List<Agendamento>> Get() =>
        await _agendamentosService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Agendamento>> Get(string id)
    {
        var agendamento = await _agendamentosService.GetAsync(id);
        if (agendamento is null) return NotFound();
        return agendamento;
    }

    [HttpGet("recurso/{resourceId:length(24)}")]
    public async Task<List<Agendamento>> GetByResource(string resourceId) =>
        await _agendamentosService.GetByResourceAsync(resourceId);

    [HttpPost]
    public async Task<IActionResult> Post(Agendamento novoAgendamento)
    {
        await _agendamentosService.CreateAsync(novoAgendamento);
        return CreatedAtAction(nameof(Get), new { id = novoAgendamento.Id }, novoAgendamento);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Agendamento agendamentoAtualizado)
    {
        var agendamento = await _agendamentosService.GetAsync(id);
        if (agendamento is null) return NotFound();

        agendamentoAtualizado.Id = agendamento.Id;
        await _agendamentosService.UpdateAsync(id, agendamentoAtualizado);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var agendamento = await _agendamentosService.GetAsync(id);
        if (agendamento is null) return NotFound();

        await _agendamentosService.RemoveAsync(id);
        return NoContent();
    }
}