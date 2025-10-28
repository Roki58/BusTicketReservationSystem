using System.Net.Sockets;

namespace BusTicketReservation.Domain.Entities;

public class BusSchedule
{
    public Guid Id { get; set; }
    public Guid BusId { get; set; }
    public Guid RouteId { get; set; }
    public DateTime JourneyDate { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public TimeSpan ArrivalTime { get; set; }

    public Bus Bus { get; set; } = null!;
    public Route Route { get; set; } = null!;
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}