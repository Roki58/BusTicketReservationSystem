using Microsoft.AspNetCore.Mvc;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Application.Contracts.DTOs;

namespace BusTicketReservation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("seat-plan/{busScheduleId}")]
    public async Task<ActionResult<SeatPlanDto>> GetSeatPlan(Guid busScheduleId)
    {
        try
        {
            var seatPlan = await _bookingService.GetSeatPlanAsync(busScheduleId);
            return Ok(seatPlan);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("book-seat")]
    public async Task<ActionResult<BookSeatResultDto>> BookSeat([FromBody] BookSeatInputDto input)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _bookingService.BookSeatAsync(input);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}