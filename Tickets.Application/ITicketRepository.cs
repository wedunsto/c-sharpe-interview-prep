using Tickets.Domain;

namespace Tickets.Application;

public interface ITicketRepository
{
    Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Ticket?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    void Add(Ticket ticket);
    void Remove(Ticket ticket);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}