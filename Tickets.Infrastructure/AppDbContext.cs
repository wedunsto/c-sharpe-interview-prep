using Microsoft.EntityFrameworkCore;
using Tickets.Domain;

namespace Tickets.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Ticket> Tickets => Set<Ticket>();
}