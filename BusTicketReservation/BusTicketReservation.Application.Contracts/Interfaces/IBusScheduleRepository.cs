namespace BusTicketReservation.Application.Contracts.Interfaces;

using BusTicketReservation.Domain.Entities;

public interface IBusScheduleRepository : IRepository<BusSchedule>
{
    Task<List<BusSchedule>> SearchSchedulesAsync(string from, string to, DateTime date);
    Task<BusSchedule?> GetScheduleWithDetailsAsync(Guid scheduleId);
}