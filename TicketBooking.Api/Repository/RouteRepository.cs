using Microsoft.EntityFrameworkCore;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface IRouteRepository
    {
        Task<IEnumerable<BusRoute>> GetAllAsync();
        Task<BusRoute> AddAsync(BusRoute BusRoute);
        BusRoute GetRouteID(Guid fromCityID, Guid toCityID, Guid coachID);
        Task<BusRoute> GetAsync(Guid guid);
    }
    public class RouteRepository : IRouteRepository
    {
        private readonly TicketBookingDataContext _dataContext;


        public RouteRepository(TicketBookingDataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<BusRoute> AddAsync(BusRoute busRoute)
        {
            busRoute.RouteID = new Guid();
            await _dataContext.AddAsync(busRoute);
            await _dataContext.SaveChangesAsync();
            return busRoute;
        }

        public async Task<IEnumerable<BusRoute>> GetAllAsync()
        {
            return await _dataContext.BusRoutes
                .Include(x => x.FromCity)
                .Include(x => x.ToCity)
                .Include(x => x.Coach)
                .ToListAsync();
        }

        public async Task<BusRoute> GetAsync(Guid guid)
        {
            return await _dataContext.BusRoutes
                .Include(x => x.FromCity)
                .Include(x => x.ToCity)
                .Include(x => x.Coach).FirstOrDefaultAsync(x => x.RouteID == guid);
        }

        public BusRoute GetRouteID(Guid fromCityID, Guid toCityID, Guid coachID)
        {
            return _dataContext.BusRoutes.FirstOrDefault(x => x.FromCityID == fromCityID && x.ToCityID == toCityID && x.CoachID == coachID);
        }
    }
}
