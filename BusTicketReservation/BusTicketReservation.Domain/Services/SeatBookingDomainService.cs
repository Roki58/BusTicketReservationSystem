namespace BusTicketReservation.Domain.Services;

using BusTicketReservation.Domain.Entities;

public class SeatBookingDomainService : ISeatBookingDomainService
{
    public Ticket BookSeat(
        Guid scheduleId,
        Guid seatId,
        Guid passengerId,
        string boardingPoint,
        string droppingPoint,
        decimal price)
    {
        return new Ticket
        {
            Id = Guid.NewGuid(),
            BusScheduleId = scheduleId,
            SeatId = seatId,
            PassengerId = passengerId,
            BoardingPoint = boardingPoint,
            DroppingPoint = droppingPoint,
            Status = SeatStatus.Booked,
            Price = price,
            BookedAt = DateTime.UtcNow
        };
    }
}