using Moq;
using Tickets.Application;
using Tickets.Domain;

namespace Tickets.Tests;

public class TicketServiceTests
{
    private readonly Mock<ITicketRepository> _repository = new();
    private readonly TicketService _service;

    public TicketServiceTests()
    {
        _service = new TicketService(_repository.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WhenTicketExists_ReturnsTicket()
    {
        var ticket = new Ticket("Login broken", "Cannot sign in");
        _repository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ticket);

        var result = await _service.GetByIdAsync(1);

        Assert.Same(ticket, result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenTicketMissing_ThrowsTicketNotFoundException()
    {
        _repository.Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Ticket?)null);

        await Assert.ThrowsAsync<TicketNotFoundException>(() => _service.GetByIdAsync(99));
    }

    [Fact]
    public async Task CreateAsync_AddsTicketAndSavesChanges()
    {
        var result = await _service.CreateAsync("Login broken", "Cannot sign in");

        Assert.Equal("Login broken", result.Title);
        Assert.Equal("OPEN", result.Status);
        _repository.Verify(r => r.Add(It.IsAny<Ticket>()), Times.Once);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CloseAsync_SetsStatusToClosedAndSavesChanges()
    {
        var ticket = new Ticket("Login broken", "Cannot sign in");
        _repository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ticket);

        var result = await _service.CloseAsync(1);

        Assert.Equal("CLOSED", result.Status);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}