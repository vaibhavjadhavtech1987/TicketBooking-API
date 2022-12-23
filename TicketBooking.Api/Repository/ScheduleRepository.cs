using Microsoft.EntityFrameworkCore;
using System;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface IScheduleRepository
    {
        Task<BusSchedule> AddAsync(BusSchedule schedule);
        Task<IEnumerable<BusSchedule>> GetAllAsync(string fromCity, string ToCity, DateTime departureDate);
        Task<IEnumerable<BusSchedule>> GetAllAsync();
    }
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly TicketBookingDataContext _dataContext;
        public ScheduleRepository(TicketBookingDataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<BusSchedule> AddAsync(BusSchedule schedule)
        {
            schedule.ScheduleID = new Guid();
            await _dataContext.AddAsync(schedule);
            await _dataContext.SaveChangesAsync();
            return schedule;
        }

        public async Task<IEnumerable<BusSchedule>> GetAllAsync()
        {
            var bookingDetails = await _dataContext.BusSchedules
                   .Include(x=>x.BookingDetails)
                   .Include(x => x.Bus)
                   .Include(x => x.Bus.Coach)
                   .Include(x => x.BusRoute)
                   .Include(x => x.BusRoute.FromCity)
                   .Include(x => x.BusRoute.ToCity).ToListAsync();

            return bookingDetails;

        }

        public async Task<IEnumerable<BusSchedule>> GetAllAsync(string fromCity,string ToCity,DateTime departureDate)
        {
            return await _dataContext.BusSchedules
                 .Include(x => x.Bus)
                 .Include(x => x.BusRoute.FromCity.CityName.Equals(fromCity) && x.BusRoute.ToCity.CityName.Equals(ToCity))
                 .Include(x => x.BookingDetails)
                .ToListAsync();
        }
    }
}
