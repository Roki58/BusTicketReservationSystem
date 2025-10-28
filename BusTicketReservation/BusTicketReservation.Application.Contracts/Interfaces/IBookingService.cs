namespace BusTicketReservation.Application.Contracts.Interfaces;

using BusTicketReservation.Application.Contracts.DTOs;

public interface IBookingService
{
    Task<SeatPlanDto> GetSeatPlanAsync(Guid busScheduleId);
    Task<BookSeatResultDto> BookSeatAsync(BookSeatInputDto input);
}