namespace BusTicketReservation.Application.Contracts.Interfaces;

using BusTicketReservation.Domain.Entities;

public interface ISeatRepository : IRepository<Seat>
{
    Task<List<Seat>> GetSeatsByBusIdAsync(Guid busId);
}