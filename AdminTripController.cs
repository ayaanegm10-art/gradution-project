using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using try4.data;
using try4.Models;
using try4.Services.Implementation;
using try4.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace try4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminTripController : ControllerBase
    {
        private readonly IAdminTripService _adminTripService;
        private readonly MyDbContext _context;
        public AdminTripController(IAdminTripService adminTripService, MyDbContext context)
        {
            _adminTripService = adminTripService;
            _context = context;
        }
        //    1      ---------------------------------------
        [HttpGet("GetAllSearches")]
        public async Task<IActionResult> GetAllSearches()
        {
            return Ok(_adminTripService.GetAllSearches());
        }
        //   2    ============================================
        [HttpGet("GetAllTrips")]
        public async Task<IActionResult> GetAllTrips()
        {
            return Ok(_adminTripService.GetAllTrips());
        }

     //   3     --------------------------------------------

        [HttpPut("AddTrip")]
        public async Task<IActionResult> AddTrip(int tripId, double tripPrice, int seats, int routeId, int fromCenterId, int TocenterId, TimeOnly departureTime, DateTime? departureDate, int totalSeats, bool isActive)
        {
            try
            {

                await _adminTripService.AddTrip(tripId, tripPrice, seats, routeId, fromCenterId, TocenterId, departureTime, departureDate, totalSeats, isActive);
                return Ok("Trip Added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

    //    4     ---------------------------------------------
        [HttpDelete("CancelTrip")]
        //[Authorize]
        public async Task<IActionResult> CancelTrip(int tripId)
        {
            try
            {

                await _adminTripService.CancelTrip(tripId);
                return Ok("Trip canceled successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

    //    5     ----------------------------------
        [HttpDelete("DeleteUser")]
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {

                await _adminTripService.DeleteUser(userId);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

    //    6      ------------------------------------
        [HttpGet("SearchTrips")]
        public async Task<List<Trip>> SearchTrips(int fromId, int toId, int seats)
        {
           

                return await _adminTripService.SearchTrips(fromId, toId, seats);
        }

    }
}
