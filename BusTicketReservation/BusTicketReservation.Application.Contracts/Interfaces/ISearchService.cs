namespace BusTicketReservation.Application.Contracts.Interfaces;

using BusTicketReservation.Application.Contracts.DTOs;

public interface ISearchService
{
    Task<List<AvailableBusDto>> SearchAvailableBusesAsync(
        string from,
        string to,
        DateTime journeyDate);
}