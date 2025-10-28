using Xunit;
using Moq;
using FluentAssertions;
using BusTicketReservation.Application.Services;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Application.Contracts.DTOs;
using BusTicketReservation.Domain.Entities;
using BusTicketReservation.Domain.Services;

namespace BusTicketReservation.Tests.Application;

public class BookingServiceTests
{
    private readonly Mock<IBusScheduleRepository> _scheduleRepoMock;
    private readonly Mock<ISeatRepository> _seatRepoMock;
    private readonly Mock<ITicketRepository> _ticketRepoMock;
    private readonly Mock<IPassengerRepository> _passengerRepoMock;
    private readonly Mock<ISeatBookingDomainService> _domainServiceMock;
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _scheduleRepoMock = new Mock<IBusScheduleRepository>();
        _seatRepoMock = new Mock<ISeatRepository>();
        _ticketRepoMock = new Mock<ITicketRepository>();
        _passengerRepoMock = new Mock<IPassengerRepository>();
        _domainServiceMock = new Mock<ISeatBookingDomainService>();

        _bookingService = new BookingService(
            _scheduleRepoMock.Object,
            _seatRepoMock.Object,
            _ticketRepoMock.Object,
            _passengerRepoMock.Object,
            _domainServiceMock.Object
        );
    }

    [Fact]
    public async Task BookSeatAsync_ShouldBookSeat_WhenSeatIsAvailable()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var seatId = Guid.NewGuid();
        var passengerId = Guid.NewGuid();

        var input = new BookSeatInputDto
        {
            BusScheduleId = scheduleId,
            SeatId = seatId,
            PassengerName = "John Doe",
            MobileNumber = "01712345678",
            BoardingPoint = "Gabtoli",
            DroppingPoint = "Nasirabad"
        };

        var passenger = new Passenger { Id = passengerId, Name = "John Doe", MobileNumber = "01712345678" };
        var schedule = new BusSchedule { Id = scheduleId, Bus = new Bus { BasePrice = 800 } };
        var seat = new Seat { Id = seatId, SeatNumber = "A1" };
        var ticket = new Ticket { Id = Guid.NewGuid(), SeatId = seatId, Status = SeatStatus.Booked };

        _ticketRepoMock.Setup(x => x.IsSeatBookedAsync(scheduleId, seatId)).ReturnsAsync(false);
        _passengerRepoMock.Setup(x => x.GetByMobileAsync(input.MobileNumber)).ReturnsAsync(passenger);
        _scheduleRepoMock.Setup(x => x.GetByIdAsync(scheduleId)).ReturnsAsync(schedule);
        _seatRepoMock.Setup(x => x.GetByIdAsync(seatId)).ReturnsAsync(seat);
        _domainServiceMock.Setup(x => x.BookSeat(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>())).Returns(ticket);
        _ticketRepoMock.Setup(x => x.AddAsync(It.IsAny<Ticket>())).ReturnsAsync(ticket);

        // Act
        var result = await _bookingService.BookSeatAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Seat booked successfully");
        result.SeatNumber.Should().Be("A1");
        result.TicketId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task BookSeatAsync_ShouldFail_WhenSeatIsAlreadyBooked()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        var seatId = Guid.NewGuid();

        var input = new BookSeatInputDto
        {
            BusScheduleId = scheduleId,
            SeatId = seatId,
            PassengerName = "John Doe",
            MobileNumber = "01712345678",
            BoardingPoint = "Gabtoli",
            DroppingPoint = "Nasirabad"
        };

        _ticketRepoMock.Setup(x => x.IsSeatBookedAsync(scheduleId, seatId)).ReturnsAsync(true);

        // Act
        var result = await _bookingService.BookSeatAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Seat is already booked");
        result.TicketId.Should().BeNull();
    }

    [Fact]
    public async Task GetSeatPlanAsync_ShouldReturnSeatPlan_WhenScheduleExists()
    {
        // Arrange
        var busId = Guid.NewGuid();
        var scheduleId = Guid.NewGuid();
        var schedule = new BusSchedule
        {
            Id = scheduleId,
            BusId = busId,
            Bus = new Bus { Id = busId, TotalSeats = 40 },
            Route = new Route { FromCity = "Dhaka", ToCity = "Chittagong" }
        };

        var seats = new List<Seat>
        {
            new Seat { Id = Guid.NewGuid(), BusId = busId, SeatNumber = "A1", RowNumber = 0, ColumnNumber = 1 },
            new Seat { Id = Guid.NewGuid(), BusId = busId, SeatNumber = "A2", RowNumber = 0, ColumnNumber = 2 }
        };

        var tickets = new List<Ticket>();

        _scheduleRepoMock.Setup(x => x.GetScheduleWithDetailsAsync(scheduleId)).ReturnsAsync(schedule);
        _seatRepoMock.Setup(x => x.GetSeatsByBusIdAsync(busId)).ReturnsAsync(seats);
        _ticketRepoMock.Setup(x => x.GetTicketsByScheduleAsync(scheduleId)).ReturnsAsync(tickets);

        // Act
        var result = await _bookingService.GetSeatPlanAsync(scheduleId);

        // Assert
        result.Should().NotBeNull();
        result.BusScheduleId.Should().Be(scheduleId);
        result.Seats.Should().HaveCount(2);
        result.Seats[0].Status.Should().Be("Available");
    }

    [Fact]
    public async Task GetSeatPlanAsync_ShouldThrowException_WhenScheduleNotFound()
    {
        // Arrange
        var scheduleId = Guid.NewGuid();
        _scheduleRepoMock.Setup(x => x.GetScheduleWithDetailsAsync(scheduleId)).ReturnsAsync((BusSchedule?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _bookingService.GetSeatPlanAsync(scheduleId));
    }
}