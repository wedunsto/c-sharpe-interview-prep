using Tickets.Domain;

namespace Tickets.Application;

public class TicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken = default)
        => _repository.GetAllAsync(cancellationToken);

    public async Task<Ticket> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var ticket = await _repository.GetByIdAsync(id, cancellationToken);
        return ticket ?? throw new TicketNotFoundException(id);
    }

    public async Task<Ticket> CreateAsync(string title, string description, CancellationToken cancellationToken = default)
    {
        var ticket = new Ticket(title, description);
        _repository.Add(ticket);
        await _repository.SaveChangesAsync(cancellationToken);
        return ticket;
    }

    public async Task<Ticket> CloseAsync(long id, CancellationToken cancellationToken = default)
    {
        var ticket = await GetByIdAsync(id, cancellationToken);
        ticket.Status = "CLOSED";
        await _repository.SaveChangesAsync(cancellationToken);
        return ticket;
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var ticket = await GetByIdAsync(id, cancellationToken);
        _repository.Remove(ticket);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}