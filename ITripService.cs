using Microsoft.AspNetCore.Mvc;
using try4.Models;

namespace try4.Services.Interfaces
{
    public interface ITripService
    {
        public List<Trip> GetAllTrips();
       
        public Task<List<Trip>> SearchTrips(int fromId, int toId, int seats);
       
       
        
    
    }
}
