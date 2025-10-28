using System.Net.Sockets;

namespace BusTicketReservation.Domain.Entities;

public class Seat
{
    public Guid Id { get; set; }
    public Guid BusId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }

    public Bus Bus { get; set; } = null!;
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}