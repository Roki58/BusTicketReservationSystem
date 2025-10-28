using Microsoft.EntityFrameworkCore;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Domain.Entities;
using BusTicketReservation.Infrastructure.Data;

namespace BusTicketReservation.Infrastructure.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(BusTicketDbContext context) : base(context) { }

    public async Task<List<Ticket>> GetTicketsByScheduleAsync(Guid scheduleId)
    {
        return await _context.Tickets.Where(t => t.BusScheduleId == scheduleId).ToListAsync();
    }

    public async Task<bool> IsSeatBookedAsync(Guid scheduleId, Guid seatId)
    {
        var res = await _context.Tickets
            .AnyAsync(t => t.BusScheduleId == scheduleId && t.SeatId == seatId && t.Status != SeatStatus.Available);
        return res;
    }
}