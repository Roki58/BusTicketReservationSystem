using Microsoft.AspNetCore.Mvc;
using BusTicketReservation.Application.Contracts.Interfaces;
using BusTicketReservation.Application.Contracts.DTOs;

namespace BusTicketReservation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusSearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public BusSearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AvailableBusDto>>> SearchBuses(
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] DateTime journeyDate)
    {
        if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            return BadRequest("From and To cities are required");

        try
        {
            var buses = await _searchService.SearchAvailableBusesAsync(from, to, journeyDate);
            return Ok(buses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}