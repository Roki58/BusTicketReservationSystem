namespace BusTicketReservation.Domain.Entities;

public class Route
{
    public Guid Id { get; set; }
    public string FromCity { get; set; } = string.Empty;
    public string ToCity { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }

    public ICollection<BusSchedule> Schedules { get; set; } = new List<BusSchedule>();
}