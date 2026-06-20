using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using try4.data;
using try4.Models;
using try4.Services.Interfaces;

namespace try4.Services.Implementation
{
    public class UserTripService : TripService, IUserTripService
    {
        private readonly MyDbContext _context;
        private readonly ILogger<UserTripService> _logger;
        //---------------------------
        public UserTripService(MyDbContext contextl, ILogger<UserTripService> logger) : base(contextl, logger) { _context = contextl; _logger = logger; }

        public async Task BookTrips(int tripId, int userId, int seats)
        {
            _context.Bookings.Add(new Booking
            {
                TripId = tripId,
                UserId = userId,
                SeatsCount = seats
            });

            await _context.SaveChangesAsync();
        }
        //---------------------------
        public List<Booking> GetMyBookings(int id)
        {
            //IActionResult obj = AuthController.Login( LoginDto dto);
            try
            {
                return _context.Bookings.Where(b => b.UserId == id).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to query Bookings from the database.");
                return new List<Booking>();
            }
        }
        //----------------------------------
        public async Task<IActionResult> CancelBooking(int bookingid, int userId)
        {
            try
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingid && b.UserId == userId);
                if (booking == null)
                {
                    return new NotFoundResult();
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to query Bookings from the database.");
                return new BadRequestResult();
            }

        }
        //----------------------------------
        public List<Center> GetActiveCenters()
        {
            try
            {
                return _context.Centers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to query Centers from the database.");
                return new List<Center>();
            }
        }
        //--------------------------------
        public async Task<List<Trip>> SearchTrips(int userId, int fromId, int toId, int seats)
        {
            try
            {
                _context.Searches.Add(new Search
                {
                    UserId = userId,
                    FromId = fromId,
                    ToId = toId,
                    Seats = seats,
                    SearchDate = DateTime.UtcNow
                });

                await _context.SaveChangesAsync();

                return await _context.Trips
                    .Where(t => t.FromCenterId == fromId &&
                                t.ToCenterId == toId &&
                                t.TotalSeats - t.BookedSeats >= seats)
                    .OrderBy(t => t.DepartureDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to search Trips.");
                return new List<Trip>();
            }
        }
        //--------------------------------
        public List<SearchDto> GetSearches(int userId)
        {
            try
            {
                var searches =
                    from search in _context.Searches.AsNoTracking()
                    join user in _context.Users.AsNoTracking()
                        on search.UserId equals user.Id
                    join fromCenter in _context.Centers.AsNoTracking()
                        on search.FromId equals fromCenter.CenterId
                    join toCenter in _context.Centers.AsNoTracking()
                        on search.ToId equals toCenter.CenterId
                    where search.UserId == userId
                    orderby search.SearchDate descending
                    select new SearchDto
                    {
                        SearchId = search.SearchId,
                        UserId = search.UserId,
                        UserFullName = user.UserFullName,
                        FromId = search.FromId,
                        FromCenterName = fromCenter.Name,
                        ToId = search.ToId,
                        ToCenterName = toCenter.Name,
                        Seats = search.Seats,
                        SearchDate = search.SearchDate
                    };

                return searches.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to query Searches from the database.");
                return new List<SearchDto>();
            }
        }
        //------------------------------

    }
}
