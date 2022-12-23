using Microsoft.EntityFrameworkCore;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface IBusRepository
    {
        Task<IEnumerable<Bus>> GetAllAsync();
        Task<Bus> GetAsync(Guid guid);
        Task<Bus> AddAsync(Bus bus);
        Task<Bus> DeleteAsync(Guid guid);
        Task<Bus> UpdateAsync(Guid guid, Bus bus);
    }
    public class BusRepository : IBusRepository
    {
        private readonly TicketBookingDataContext _dataContext;
        public BusRepository(TicketBookingDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Bus>> GetAllAsync()
        {
            return await _dataContext.Buses
                    .Include(x => x.Coach)
                    .ToListAsync();
        }

        public async Task<Bus> GetAsync(Guid guid)
        {
            var bus = await _dataContext.Buses.FirstOrDefaultAsync(x => x.BusID == guid);
            return bus;
        }
        public async Task<Bus> AddAsync(Bus bus)
        {
            bus.BusID = new Guid();
            await _dataContext.AddAsync(bus);
            await _dataContext.SaveChangesAsync();
            return bus;
        }

        public async Task<Bus> DeleteAsync(Guid guid)
        {
            var bus = await GetAsync(guid);
            if (bus == null)
            {
                return null;
            }
            _dataContext.Buses.Remove(bus);
            await _dataContext.SaveChangesAsync();
            return bus;
        }

        public async Task<Bus> UpdateAsync(Guid guid, Bus bus)
        {
            var existingbus = await _dataContext.Buses.FirstOrDefaultAsync(x => x.BusID == guid);
            if (existingbus == null)
            {
                return null;
            }
            existingbus.BusName = bus.BusName;
            existingbus.BusNumber = bus.BusNumber;
            existingbus.CoachID = bus.CoachID;
            existingbus.TotalSeat = bus.TotalSeat;
            existingbus.UpdateTime = bus.UpdateTime;
            await _dataContext.SaveChangesAsync();
            return bus;
        }
    }
}
