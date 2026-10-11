using Microsoft.AspNetCore.Mvc;
using Tickets.Application;
using Tickets.Domain;

namespace Tickets.Api.Controllers;

[ApiController]
[Route("tickets")]
public class TicketsController : ControllerBase
{
    private readonly TicketService _service;

    public TicketsController(TicketService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Ticket>>> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetById(long id, CancellationToken cancellationToken)
        => Ok(await _service.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<Ticket>> Create(CreateTicketRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _service.CreateAsync(request.Title, request.Description, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id}/close")]
    public async Task<ActionResult<Ticket>> Close(long id, CancellationToken cancellationToken)
        => Ok(await _service.CloseAsync(id, cancellationToken));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}