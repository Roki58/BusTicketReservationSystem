namespace BusTicketReservation.Application.Services;

using BusTicketReservation.Application.Contracts.DTOs;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Domain.Entities;
using BusTicketReservation.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBusScheduleRepository _scheduleRepo;
    private readonly ISeatRepository _seatRepo;
    private readonly ITicketRepository _ticketRepo;
    private readonly IPassengerRepository _passengerRepo;
    private readonly ISeatBookingDomainService _domainService;

    public BookingService(
        IBusScheduleRepository scheduleRepo,
        ISeatRepository seatRepo,
        ITicketRepository ticketRepo,
        IPassengerRepository passengerRepo,
        ISeatBookingDomainService domainService)
    {
        _scheduleRepo = scheduleRepo;
        _seatRepo = seatRepo;
        _ticketRepo = ticketRepo;
        _passengerRepo = passengerRepo;
        _domainService = domainService;
    }

    public async Task<SeatPlanDto> GetSeatPlanAsync(Guid busScheduleId)
    {
        var schedule = await _scheduleRepo.GetScheduleWithDetailsAsync(busScheduleId);
        if (schedule == null)
            throw new ArgumentException("Schedule not found");

        var seats = await _seatRepo.GetSeatsByBusIdAsync(schedule.BusId);
        var tickets = await _ticketRepo.GetTicketsByScheduleAsync(busScheduleId);

        var bookedSeatIds = tickets
            .Where(t => t.Status != SeatStatus.Available)
            .Select(t => t.SeatId)
            .ToHashSet();

        var seatDtos = seats.Select(s => new SeatDto
        {
            SeatId = s.Id,
            SeatNumber = s.SeatNumber,
            RowNumber = s.RowNumber,
            ColumnNumber = s.ColumnNumber,
            Status = bookedSeatIds.Contains(s.Id) ? "Booked" : "Available"
        }).ToList();

        return new SeatPlanDto
        {
            BusScheduleId = busScheduleId,
            Seats = seatDtos
        };
    }

    public async Task<BookSeatResultDto> BookSeatAsync(BookSeatInputDto input)
    {
        var isBooked = await _ticketRepo.IsSeatBookedAsync(
            input.BusScheduleId,
            input.SeatId);

        if (isBooked)
        {
            return new BookSeatResultDto
            {
                Success = false,
                Message = "Seat is already booked"
            };
        }

        var passenger = await _passengerRepo.GetByMobileAsync(input.MobileNumber);
        if (passenger == null)
        {
            passenger = await _passengerRepo.AddAsync(new Passenger
            {
                Id = Guid.NewGuid(),
                Name = input.PassengerName,
                MobileNumber = input.MobileNumber
            });
        }

        var schedule = await _scheduleRepo.GetByIdAsync(input.BusScheduleId);
        var seat = await _seatRepo.GetByIdAsync(input.SeatId);

        if (schedule == null || seat == null)
        {
            return new BookSeatResultDto
            {
                Success = false,
                Message = "Invalid schedule or seat"
            };
        }

        var ticket = _domainService.BookSeat(
            input.BusScheduleId,
            input.SeatId,
            passenger.Id,
            input.BoardingPoint,
            input.DroppingPoint,
            schedule.Bus.BasePrice);

        await _ticketRepo.AddAsync(ticket);

        return new BookSeatResultDto
        {
            Success = true,
            Message = "Seat booked successfully",
            TicketId = ticket.Id,
            SeatNumber = seat.SeatNumber
        };
    }
}