using Microsoft.EntityFrameworkCore;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface ICoachRepository
    {
        Task<IEnumerable<Coach>> GetAllAsync();
        Task<Coach> GetAsync(Guid guid);
        Task<Coach> AddAsync(Coach city);
        Task<Coach> DeleteAsync(Guid guid);
        Task<Coach> UpdateAsync(Guid guid, Coach city);
    }
    public class CoachRepository : ICoachRepository
    {
        private readonly TicketBookingDataContext _dataContext;
        public CoachRepository(TicketBookingDataContext dataContext)
        {
            this._dataContext = dataContext;

        }
        public async Task<IEnumerable<Coach>> GetAllAsync()
        {
            return await this._dataContext.Coaches.ToListAsync();
        }

        public async Task<Coach> GetAsync(Guid guid)
        {
            var coach = await _dataContext.Coaches.FirstOrDefaultAsync(x => x.CoachID == guid);
            return coach;
        }
        public async Task<Coach> AddAsync(Coach coach)
        {
            coach.CoachID = new Guid();
            await this._dataContext.AddAsync(coach);
            await _dataContext.SaveChangesAsync();
            return coach;
        }

        public async Task<Coach> DeleteAsync(Guid guid)
        {
            var coach = await GetAsync(guid);
            if (coach == null)
            {
                return null;
            }
            _dataContext.Coaches.Remove(coach);
            await _dataContext.SaveChangesAsync();
            return coach;
        }

        public async Task<Coach> UpdateAsync(Guid guid, Coach city)
        {
            var existingcoach = await _dataContext.Coaches.FirstOrDefaultAsync(x => x.CoachID == guid);
            if (existingcoach == null)
            {
                return null;
            }
            existingcoach.CoachName = city.CoachName;
            await _dataContext.SaveChangesAsync();
            return city;
        }
    }
}
