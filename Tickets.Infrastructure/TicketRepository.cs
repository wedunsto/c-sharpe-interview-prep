using Microsoft.EntityFrameworkCore;
using Tickets.Application;
using Tickets.Domain;

namespace Tickets.Infrastructure;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken = default)
        => _context.Tickets.AsNoTracking().ToListAsync(cancellationToken);

    public Task<Ticket?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => _context.Tickets.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public void Add(Ticket ticket)
        => _context.Tickets.Add(ticket);

    public void Remove(Ticket ticket) => _context.Tickets.Remove(ticket);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}