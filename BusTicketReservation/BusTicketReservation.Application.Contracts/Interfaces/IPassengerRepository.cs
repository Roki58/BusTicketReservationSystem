namespace BusTicketReservation.Application.Contracts.Interfaces;

using BusTicketReservation.Domain.Entities;

public interface IPassengerRepository : IRepository<Passenger>
{
    Task<Passenger?> GetByMobileAsync(string mobile);
}