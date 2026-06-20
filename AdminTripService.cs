using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using try4.data;
using try4.Models;
using try4.Services.Interfaces;
namespace try4.Services.Implementation
{
    public class AdminTripService: TripService , IAdminTripService
    {
        private readonly MyDbContext _context;
        private readonly ILogger<AdminTripService> _logger;

        public AdminTripService(MyDbContext context, ILogger<AdminTripService> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        //  ============================================================================
        public List<SearchDto> GetAllSearches()
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

        //    ============================================================================
        public async Task AddTrip(int tripId, double tripPrice, int seats, int routeId, int fromCenterId, int TocenterId, TimeOnly departureTime, DateTime? departureDate, int totalSeats, bool isActive)
        {
            _context.Trips.Add(new Trip
            {
                TripId = tripId,
                TripPrice = tripPrice,
                RouteId = routeId,
                FromCenterId = fromCenterId,
                ToCenterId = TocenterId,
                DepartureTime = departureTime,
                DepartureDate = departureDate,
                TotalSeats = totalSeats,
                IsActive = isActive
            });

            await _context.SaveChangesAsync();
        }

     //============================================================================
        public async Task<IActionResult> CancelTrip(int tripId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(b => b.TripId == tripId);
                if (trip == null)
                {
                    return new NotFoundResult();
                }

                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to query Trips from the database.");
                return new BadRequestResult();
            }

        }

    //============================================================================
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(b => b.Id == userId);
                if (user == null)
                {
                    return new NotFoundResult();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to query Users from the database.");
                return new BadRequestResult();
            }

        }

    //--------------------------------------------------------------
        public async Task<List<Trip>> SearchTrips(int userId, int fromId, int toId, int seats)
        {
            try
            {
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
    
    //============================================================================
        public async Task<IActionResult> MakeAdmin(int adminId, int userId)
        {
            try
            {
                var admin = await _context.Users.FirstOrDefaultAsync(u => u.Id == adminId);
                if (admin == null || admin.Role != UserRole.Admin)
                {
                    return new UnauthorizedResult();
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return new NotFoundResult();
                }

                user.Role = UserRole.Admin;
                await _context.SaveChangesAsync();

                return new OkObjectResult(new
                {
                    user.Id,
                    user.Email,
                    user.UserFullName,
                    user.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to make user admin.");
                return new BadRequestResult();
            }
        }

    //--------------------------------------------------------------
       

    }
}
