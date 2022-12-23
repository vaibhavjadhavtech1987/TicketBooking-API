using Microsoft.EntityFrameworkCore;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetAsync(Guid guid);
        Task<City> AddAsync(City city);
        Task<City> DeleteAsync(Guid guid);
        Task<City> UpdateAsync(Guid guid, City city);
    }
    public class CityRepository : ICityRepository
    {
        private readonly TicketBookingDataContext _dataContext;


        public CityRepository(TicketBookingDataContext dataContext)
        {
            this._dataContext = dataContext;

        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _dataContext.Cities.ToListAsync();
        }

        public async Task<City> GetAsync(Guid guid)
        {
            var city = await _dataContext.Cities.FirstOrDefaultAsync(x => x.CityID == guid);
            return city;
        }

        public async Task<City> AddAsync(City city)
        {
            city.CityID = new Guid();
            await _dataContext.AddAsync(city);
            await _dataContext.SaveChangesAsync();
            return city;
        }

        public async Task<City> DeleteAsync(Guid guid)
        {
            var city = await GetAsync(guid);
            if (city == null)
            {
                return null;
            }
            _dataContext.Cities.Remove(city);
            await _dataContext.SaveChangesAsync();
            return city;
        }

        public async Task<City> UpdateAsync(Guid guid, City city)
        {
            var existingcity = await _dataContext.Cities.FirstOrDefaultAsync(x => x.CityID == guid);
            if (existingcity == null)
            {
                return null;
            }
            existingcity.CityName = city.CityName;
            //_dataContext.Cities.Update(existingcity);
            await _dataContext.SaveChangesAsync();
            return city;
        }
    }
}
