using Microsoft.EntityFrameworkCore;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Domain.Entities;
using BusTicketReservation.Infrastructure.Data;

namespace BusTicketReservation.Infrastructure.Repositories;

public class SeatRepository : Repository<Seat>, ISeatRepository
{
    public SeatRepository(BusTicketDbContext context) : base(context) { }

    public async Task<List<Seat>> GetSeatsByBusIdAsync(Guid busId)
    {
        return await _context.Seats
            .Where(s => s.BusId == busId)
            .OrderBy(s => s.RowNumber)
            .ThenBy(s => s.ColumnNumber)
            .ToListAsync();
    }
}