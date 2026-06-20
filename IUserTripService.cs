using Microsoft.AspNetCore.Mvc;
using try4.Models;
namespace try4.Services.Interfaces
{
    public interface IUserTripService 
    {
        public Task BookTrips(int tripId, int userId, int seats);
        public List<Booking> GetMyBookings(int id);
        public Task<IActionResult> CancelBooking(int bookingid, int userId);
        public List<Center> GetActiveCenters();
        public List<Trip> GetAllTrips();
        public Task<List<Trip>> SearchTrips(int userId, int fromId, int toId, int seats);
        public List<SearchDto> GetSearches(int userId);

    }
}
