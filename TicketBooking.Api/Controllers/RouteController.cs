using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TicketBooking.Api.Entities;
using TicketBooking.Api.Repository;
using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : Controller
    {
        private readonly IRouteRepository _routeRepository;
        public RouteController(IRouteRepository routeRepository)
        {
            this._routeRepository = routeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoutesAsync()
        {
            var routes = await this._routeRepository.GetAllAsync();
            var routeDtos = new List<BusRouteVM>();
            foreach (var route in routes)
            {
               var busRouteDto =  MapEntity(route);
                routeDtos.Add(busRouteDto);
            }
            return Ok(routeDtos);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid guid)
        {
            var routes = await this._routeRepository.GetAllAsync();
            var routeDtos = new List<BusRouteVM>();
            foreach (var route in routes)
            {
                var busRouteDto = MapEntity(route);
                routeDtos.Add(busRouteDto);
            }
            return Ok(routeDtos);
        }
        [HttpPost]
        public async Task<IActionResult> AddRouteAsync(BusRouteAddVM route)
        {
            var _route = new BusRoute
            {
                FromCityID = route.FromCityID,
                ToCityID = route.ToCityID,
                CoachID = route.CoachID,
                Price = route.Price,
                UpdateTime = DateTime.Now
            };
            var _routeresult = await this._routeRepository.AddAsync(_route);
            var _routeVM = new BusRouteVM
            {
                RouteID = _routeresult.RouteID,
                FromCity =(_routeresult.FromCity != null? new ScheduleCityVM { CityID = _routeresult.FromCity.CityID, CityName = _routeresult.FromCity.CityName } : null) ,
                ToCity =  (_routeresult.ToCity != null ?  new ScheduleCityVM { CityID = _routeresult.ToCity.CityID, CityName = _routeresult.ToCity.CityName } : null),
            };
            return Ok(_routeVM);
        }
        private BusRouteVM MapEntity(BusRoute busRoute)
        {
            BusRouteVM busRouteDto = new BusRouteVM();
            busRouteDto.RouteID = busRoute.RouteID;
            busRouteDto.Price = busRoute.Price;
            busRouteDto.UpdateTime = busRoute.UpdateTime;
            busRouteDto.FromCity = new ScheduleCityVM { CityID = busRoute.FromCity.CityID, CityName = busRoute.FromCity.CityName };
            busRouteDto.ToCity = new ScheduleCityVM { CityID = busRoute.ToCity.CityID, CityName = busRoute.ToCity.CityName };
            busRouteDto.Coach = new ScheduleCoachVM { CoachID = busRoute.Coach.CoachID, CoachName = busRoute.Coach.CoachName };
            return busRouteDto;
        }
    }
}
