using Xunit;
using FluentAssertions;
using BusTicketReservation.Domain.Services;
using BusTicketReservation.Domain.Entities;

namespace BusTicketReservation.Tests.Domain;

public class SeatBookingDomainServiceTests
{
    private readonly SeatBookingDomainService _domainService;

    public SeatBookingDomainServiceTests()
    {
        _domainService = new SeatBookingDomainService();
    }

    [Fact]
    public void BookSeat_ShouldCreateTicketWithCorrectDetails()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var seatId = Guid.NewGuid();
        var passengerId = Guid.NewGuid();
        var boardingPoint = "Gabtoli";
        var droppingPoint = "Nasirabad";
        var price = 800m;

        // Act
        var ticket = _domainService.BookSeat(scheduleId, seatId, passengerId, boardingPoint, droppingPoint, price);

        // Assert
        ticket.Should().NotBeNull();
        ticket.Id.Should().NotBeEmpty();
        ticket.BusScheduleId.Should().Be(scheduleId);
        ticket.SeatId.Should().Be(seatId);
        ticket.PassengerId.Should().Be(passengerId);
        ticket.BoardingPoint.Should().Be(boardingPoint);
        ticket.DroppingPoint.Should().Be(droppingPoint);
        ticket.Price.Should().Be(price);
        ticket.Status.Should().Be(SeatStatus.Booked);
        ticket.BookedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void BookSeat_ShouldSetStatusToBooked()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var seatId = Guid.NewGuid();
        var passengerId = Guid.NewGuid();

        // Act
        var ticket = _domainService.BookSeat(scheduleId, seatId, passengerId, "A", "B", 800);

        // Assert
        ticket.Status.Should().Be(SeatStatus.Booked);
    }
}