using Microsoft.EntityFrameworkCore;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Domain.Entities;
using BusTicketReservation.Infrastructure.Data;

namespace BusTicketReservation.Infrastructure.Repositories;

public class BusScheduleRepository : Repository<BusSchedule>, IBusScheduleRepository
{
    public BusScheduleRepository(BusTicketDbContext context) : base(context) { }

    public async Task<List<BusSchedule>> SearchSchedulesAsync(string from, string to, DateTime date)
    {
        return await _context.BusSchedules
            .Include(s => s.Bus)
            .Include(s => s.Route)
            .Where(s => s.Route.FromCity == from && s.Route.ToCity == to )
            .ToListAsync();
    }

    public async Task<BusSchedule?> GetScheduleWithDetailsAsync(Guid scheduleId)
    {
        return await _context.BusSchedules
            .Include(s => s.Bus)
            .Include(s => s.Route)
            .FirstOrDefaultAsync(s => s.Id == scheduleId);
    }
}
