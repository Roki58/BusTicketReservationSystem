using System.Net.Sockets;

namespace BusTicketReservation.Domain.Entities;

public class Passenger
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}