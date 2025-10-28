using BusTicketReservation.Domain.Entities;

namespace BusTicketReservation.Domain.Services;

public interface ISeatBookingDomainService
{
    Ticket BookSeat(
        Guid scheduleId,
        Guid seatId,
        Guid passengerId,
        string boardingPoint,
        string droppingPoint,
        decimal price);
}