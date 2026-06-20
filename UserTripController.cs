using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using try4.Controllers;
using try4.Models;
using try4.Services.Interfaces;
using try4.Services.Implementation;
namespace try4.data;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "User")]
public class UserTripController : ControllerBase
{
    private readonly IUserTripService _usertripService;
    private readonly MyDbContext _context;
    

    public UserTripController(IUserTripService userTripService , MyDbContext context)
    {
        _usertripService = userTripService;
        _context = context;
        
    }
    //——————————————————————————————————————————————————————————————————————————————

    //---------------------------------------
    [HttpGet("GetAllTrips")]
    public async Task<IActionResult> GetAllTrips()
    {
        return Ok(_usertripService.GetAllTrips());
    }

    //——————————————————————————————————————————————————————————————————————————————

    [HttpPut("BookTrip")]
    public async Task<IActionResult> BookTrips(int tripId, int seats)
    {
        try
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            await _usertripService.BookTrips(tripId, userId.Value, seats);
            return Ok("Trip booked successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }


    //——————————————————————————————————————————————————————————————————————————————

    
    [HttpGet("GetActiveCenters")]
    public async Task<IActionResult> GetActiveCenters()
    {
        var centers =  _usertripService.GetActiveCenters();
        return Ok(centers);

    }

    //——————————————————————————————————————————————————————————————————————————————
    private int? GetCurrentUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(userId, out var id) ? id : null;
    }
    //----------------------------------------------------------
    [HttpGet("GetMyBookings")]
    public async Task<IActionResult> GetMyBookings()
    {
        var id = GetCurrentUserId();
                if (id == null) return Unauthorized();
        var user_bookings = _usertripService.GetMyBookings(id.Value);
        return Ok(user_bookings);
    }
    //——————————————————————————————————————————————————————————————————————————————

    [HttpDelete("CancelMyBooking")]
    //[Authorize]
    public async Task<IActionResult> CancelMyBooking(int bookingId)
    {
        try
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            await _usertripService.CancelBooking(bookingId, userId.Value);
            return Ok("Trip canceled successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }
    //---------------------------------
    [HttpGet("Search")]
    public async Task<IActionResult> SearchForTrips([FromQuery] int fromId, [FromQuery] int toId, [FromQuery] int seats)
    {
        var userId = GetCurrentUserId();
        if (userId == null) return Unauthorized();

        var trips = await _usertripService.SearchTrips(userId.Value, fromId, toId, seats);
        return Ok(trips);
    }
    //-------------------------------
    [HttpGet("GetMySearches")]
    public async Task<IActionResult> GetMySearches()
    {
        var userId = GetCurrentUserId();
        if (userId == null) return Unauthorized();
        var searches = _usertripService.GetSearches(userId.Value);
        return Ok(searches);
    }
}




