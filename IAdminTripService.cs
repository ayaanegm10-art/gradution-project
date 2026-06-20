using Microsoft.AspNetCore.Mvc;
using try4.Models;
namespace try4.Services.Interfaces
{
    public interface IAdminTripService
    {
        public Task AddTrip(int tripId, double tripPrice, int seats, int routeId, int fromCenterId, int TocenterId, TimeOnly departureTime, DateTime? departureDate, int totalSeats, bool isActive);
        public Task<List<Trip>> SearchTrips(int fromId, int toId, int seats);
        public List<Trip> GetAllTrips();
        public Task<IActionResult> CancelTrip(int tripId);
        public Task<IActionResult> DeleteUser(int userId);
        public Task<IActionResult> MakeAdmin(int adminId, int userId);
        public List<SearchDto> GetAllSearches();



    }
}
