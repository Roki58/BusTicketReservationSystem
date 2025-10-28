namespace BusTicketReservation.Application.Services;

using BusTicketReservation.Application.Contracts.DTOs;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Domain.Entities;

public class SearchService : ISearchService
{
    private readonly IBusScheduleRepository _scheduleRepo;
    private readonly ITicketRepository _ticketRepo;

    public SearchService(
        IBusScheduleRepository scheduleRepo,
        ITicketRepository ticketRepo)
    {
        _scheduleRepo = scheduleRepo;
        _ticketRepo = ticketRepo;
    }

    public async Task<List<AvailableBusDto>> SearchAvailableBusesAsync(
        string from,
        string to,
        DateTime journeyDate)
    {
        var schedules = await _scheduleRepo.SearchSchedulesAsync(from, to, journeyDate);
        var result = new List<AvailableBusDto>();

        foreach (var schedule in schedules)
        {
            var tickets = await _ticketRepo.GetTicketsByScheduleAsync(schedule.Id);
            var bookedSeats = tickets.Count(t => t.Status != SeatStatus.Available);
            var seatsLeft = schedule.Bus.TotalSeats - bookedSeats;

            result.Add(new AvailableBusDto
            {
                BusScheduleId = schedule.Id,
                CompanyName = schedule.Bus.CompanyName,
                BusName = schedule.Bus.BusName,
                StartTime = schedule.DepartureTime.ToString(@"hh\:mm"),
                ArrivalTime = schedule.ArrivalTime.ToString(@"hh\:mm"),
                SeatsLeft = seatsLeft,
                Price = schedule.Bus.BasePrice
            });
        }

        return result;
    }
}