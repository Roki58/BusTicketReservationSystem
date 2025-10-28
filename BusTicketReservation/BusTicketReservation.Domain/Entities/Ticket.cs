namespace BusTicketReservation.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid BusScheduleId { get; set; }
    public Guid SeatId { get; set; }
    public Guid PassengerId { get; set; }
    public string BoardingPoint { get; set; } = string.Empty;
    public string DroppingPoint { get; set; } = string.Empty;
    public SeatStatus Status { get; set; }
    public decimal Price { get; set; }
    public DateTime BookedAt { get; set; }

    public BusSchedule BusSchedule { get; set; } = null!;
    public Seat Seat { get; set; } = null!;
    public Passenger Passenger { get; set; } = null!;
}