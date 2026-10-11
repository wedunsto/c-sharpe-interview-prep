namespace Tickets.Application;

public class TicketNotFoundException : Exception
{
    public TicketNotFoundException(long id)
        : base($"Ticket not found: {id}") { }
}