using Microsoft.EntityFrameworkCore;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Domain.Entities;
using BusTicketReservation.Infrastructure.Data;

namespace BusTicketReservation.Infrastructure.Repositories;

public class PassengerRepository : Repository<Passenger>, IPassengerRepository
{
    public PassengerRepository(BusTicketDbContext context) : base(context) { }

    public async Task<Passenger?> GetByMobileAsync(string mobile)
    {
        return await _context.Passengers.FirstOrDefaultAsync(p => p.MobileNumber == mobile);
    }
}