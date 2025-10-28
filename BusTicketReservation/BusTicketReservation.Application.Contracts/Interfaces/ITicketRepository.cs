namespace BusTicketReservation.Application.Contracts.Interfaces;

using BusTicketReservation.Domain.Entities;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<List<Ticket>> GetTicketsByScheduleAsync(Guid scheduleId);
    Task<bool> IsSeatBookedAsync(Guid scheduleId, Guid seatId);
}