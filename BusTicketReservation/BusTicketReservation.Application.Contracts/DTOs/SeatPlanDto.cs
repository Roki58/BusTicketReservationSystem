namespace BusTicketReservation.Application.Contracts.DTOs;

public class SeatPlanDto
{
    public Guid BusScheduleId { get; set; }
    public List<SeatDto> Seats { get; set; } = new();
}

public class SeatDto
{
    public Guid SeatId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public string Status { get; set; } = string.Empty;
}