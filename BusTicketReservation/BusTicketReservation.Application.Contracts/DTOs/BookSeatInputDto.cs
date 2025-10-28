namespace BusTicketReservation.Application.Contracts.DTOs;

public class BookSeatInputDto
{
    public Guid BusScheduleId { get; set; }
    public Guid SeatId { get; set; }
    public string PassengerName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string BoardingPoint { get; set; } = string.Empty;
    public string DroppingPoint { get; set; } = string.Empty;
}