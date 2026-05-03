using Microsoft.AspNetCore.Mvc;
using SistemaReservas.Api.Models;
using SistemaReservas.Api.Services;

namespace SistemaReservas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecursosController : ControllerBase
{
    private readonly RecursosService _recursosService;

    public RecursosController(RecursosService recursosService)
    {
        _recursosService = recursosService;
    }

    [HttpGet]
    public async Task<List<Recurso>> Get() => 
        await _recursosService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Recurso>> Get(string id)
    {
        var recurso = await _recursosService.GetAsync(id);

        if (recurso is null) return NotFound();

        return recurso;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Recurso novoRecurso)
    {
        await _recursosService.CreateAsync(novoRecurso);
        return CreatedAtAction(nameof(Get), new { id = novoRecurso.Id }, novoRecurso);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Recurso recursoAtualizado)
    {
        var recurso = await _recursosService.GetAsync(id);

        if (recurso is null) return NotFound();

        recursoAtualizado.Id = recurso.Id;

        await _recursosService.UpdateAsync(id, recursoAtualizado);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var recurso = await _recursosService.GetAsync(id);

        if (recurso is null) return NotFound();

        await _recursosService.RemoveAsync(id);

        return NoContent();
    }
}