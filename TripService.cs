using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using try4.Controllers;
using try4.data;
using try4.Models;
using try4.Services.Interfaces;
using static try4.Controllers.AuthController;

namespace try4.Services.Implementation
{
    public class TripService : ITripService
    {
        private readonly MyDbContext _context;
        private readonly ILogger<TripService> _logger;

        public TripService(MyDbContext context, ILogger<TripService> logger)
        {
            _context = context;
            _logger = logger;
        }
        // TS1 ---------------------------------
        public virtual List<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to query Trips from the database.");
                return new List<Trip>();
            }
        }
        // TS2 ---------------------------------
        public async virtual Task<List<Trip>> SearchTrips(int fromId, int toId, int seats)
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
        //------------------------------
    }
}
