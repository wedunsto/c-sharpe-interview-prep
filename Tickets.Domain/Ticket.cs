namespace Tickets.Domain;

public class Ticket
{
    public long Id { get; private set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "OPEN";

    private Ticket() { }

    public Ticket(string title, string description)
    {
        Title = title;
        Description = description;
    }
}