using Xunit;
using Moq;
using FluentAssertions;
using BusTicketReservation.Application.Services;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Application.Contracts.DTOs;
using BusTicketReservation.Domain.Entities;

namespace BusTicketReservation.Tests.Application;

public class SearchServiceTests
{
    private readonly Mock<IBusScheduleRepository> _scheduleRepoMock;
    private readonly Mock<ITicketRepository> _ticketRepoMock;
    private readonly SearchService _searchService;

    public SearchServiceTests()
    {
        _scheduleRepoMock = new Mock<IBusScheduleRepository>();
        _ticketRepoMock = new Mock<ITicketRepository>();
        _searchService = new SearchService(_scheduleRepoMock.Object, _ticketRepoMock.Object);
    }

    [Fact]
    public async Task SearchAvailableBusesAsync_ShouldReturnBuses_WhenBusesExist()
    {
        // Arrange
        var from = "Dhaka";
        var to = "Chittagong";
        var journeyDate = DateTime.Today.AddDays(1);

        var bus = new Bus
        {
            Id = Guid.NewGuid(),
            CompanyName = "Green Line",
            BusName = "GL Express",
            TotalSeats = 40,
            BasePrice = 800
        };

        var route = new Route
        {
            Id = Guid.NewGuid(),
            FromCity = from,
            ToCity = to,
            DurationMinutes = 360
        };

        var schedule = new BusSchedule
        {
            Id = Guid.NewGuid(),
            BusId = bus.Id,
            RouteId = route.Id,
            JourneyDate = journeyDate,
            DepartureTime = new TimeSpan(8, 0, 0),
            ArrivalTime = new TimeSpan(14, 0, 0),
            Bus = bus,
            Route = route
        };

        var schedules = new List<BusSchedule> { schedule };
        var tickets = new List<Ticket>();

        _scheduleRepoMock
            .Setup(x => x.SearchSchedulesAsync(from, to, journeyDate))
            .ReturnsAsync(schedules);

        _ticketRepoMock
            .Setup(x => x.GetTicketsByScheduleAsync(schedule.Id))
            .ReturnsAsync(tickets);

        // Act
        var result = await _searchService.SearchAvailableBusesAsync(from, to, journeyDate);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].CompanyName.Should().Be("Green Line");
        result[0].BusName.Should().Be("GL Express");
        result[0].SeatsLeft.Should().Be(40);
        result[0].Price.Should().Be(800);
    }

    [Fact]
    public async Task SearchAvailableBusesAsync_ShouldReturnEmptyList_WhenNoBusesAvailable()
    {
        // Arrange
        var from = "Dhaka";
        var to = "Sylhet";
        var journeyDate = DateTime.Today.AddDays(1);

        _scheduleRepoMock
            .Setup(x => x.SearchSchedulesAsync(from, to, journeyDate))
            .ReturnsAsync(new List<BusSchedule>());

        // Act
        var result = await _searchService.SearchAvailableBusesAsync(from, to, journeyDate);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchAvailableBusesAsync_ShouldCalculateCorrectSeatsLeft()
    {
        // Arrange
        var from = "Dhaka";
        var to = "Chittagong";
        var journeyDate = DateTime.Today.AddDays(1);

        var bus = new Bus { Id = Guid.NewGuid(), CompanyName = "Test", BusName = "Test", TotalSeats = 40, BasePrice = 800 };
        var route = new Route { Id = Guid.NewGuid(), FromCity = from, ToCity = to, DurationMinutes = 360 };
        var schedule = new BusSchedule
        {
            Id = Guid.NewGuid(),
            BusId = bus.Id,
            RouteId = route.Id,
            JourneyDate = journeyDate,
            DepartureTime = new TimeSpan(8, 0, 0),
            ArrivalTime = new TimeSpan(14, 0, 0),
            Bus = bus,
            Route = route
        };

        var bookedTickets = new List<Ticket>
        {
            new Ticket { Id = Guid.NewGuid(), Status = SeatStatus.Booked },
            new Ticket { Id = Guid.NewGuid(), Status = SeatStatus.Booked },
            new Ticket { Id = Guid.NewGuid(), Status = SeatStatus.Sold }
        };

        _scheduleRepoMock.Setup(x => x.SearchSchedulesAsync(from, to, journeyDate)).ReturnsAsync(new List<BusSchedule> { schedule });
        _ticketRepoMock.Setup(x => x.GetTicketsByScheduleAsync(schedule.Id)).ReturnsAsync(bookedTickets);

        // Act
        var result = await _searchService.SearchAvailableBusesAsync(from, to, journeyDate);

        // Assert
        result[0].SeatsLeft.Should().Be(37); // 40 - 3 booked tickets
    }
}