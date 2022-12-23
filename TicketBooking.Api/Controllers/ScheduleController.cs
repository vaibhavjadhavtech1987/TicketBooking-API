using Microsoft.AspNetCore.Mvc;
using TicketBooking.Api.Entities;
using TicketBooking.Api.Repository;
using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRouteRepository _routeRepository;
        public ScheduleController(IScheduleRepository scheduleRepository, IRouteRepository routeRepository)
        {
            this._scheduleRepository = scheduleRepository;
            this._routeRepository = routeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var schedules = await this._scheduleRepository.GetAllAsync();
            var schedulesVM = new List<ScheduleGetVM>();
            foreach (var busSchedule in schedules)
            {
                var dto = MapEntity(busSchedule);
                schedulesVM.Add(dto);
            }
            return Ok(schedulesVM);
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetAllScheduleAsync(string fromCity, string toCity, DateTime departureDate)
        {
            var schedules = await this._scheduleRepository.GetAllAsync(fromCity, toCity, departureDate);
            var schedulesVM = new List<ScheduleGetVM>();
            if (schedules != null)
            {
                foreach (var busSchedule in schedules)
                {
                    var dto = MapEntity(busSchedule);
                    schedulesVM.Add(dto);
                }
            }
            return Ok(schedulesVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ScheduleAddVM schedule)
        {
            var _route = this._routeRepository.GetRouteID(schedule.FromCityID, schedule.ToCityID, schedule.CoachID);
            if (_route == null)
            {
                return NotFound("routes not configured, for given inputs");
            }
            var _schedule = new BusSchedule
            {
                DepartureTime = schedule.DepartureTime,
                RouteID = _route.RouteID,
                BusID = schedule.BusID,
                UpdateDate = DateTime.Now
            };

            //var _scheduleresult = await this._scheduleRepository.AddAsync(_schedule);
            //ScheduleDTO dto = new ScheduleDTO();
            //dto.ScheduleID = _scheduleresult?.ScheduleID;
            //dto.TotalSeat = _scheduleresult?.BuseAssigned?.TotalSeat;
            //dto.ReserveSeats = 0;
            //dto.Price = _scheduleresult.BusRoute.Price;
            return Ok();
        }

        private int GetTotalReservedSeats(ICollection<BookingDetails> BookingDetails)
        {
            return 0;
        }
        private ScheduleGetVM MapEntity(BusSchedule busSchedule)
        {
            ScheduleGetVM dto = new ScheduleGetVM();
            dto.ScheduleID = busSchedule?.ScheduleID;
            dto.TotalSeat = busSchedule?.Bus?.TotalSeat;
            dto.ReserveSeats = GetTotalReservedSeats(busSchedule?.BookingDetails);
            dto.Price = busSchedule.BusRoute.Price;
            dto.DepartureTime = busSchedule.DepartureTime;
            dto.FromCity = new ScheduleCityVM
            {
                CityID = busSchedule.BusRoute.FromCity.CityID,
                CityName = busSchedule.BusRoute.FromCity.CityName
            };

            dto.ToCity = new ScheduleCityVM
            {
                CityID = busSchedule.BusRoute.ToCity.CityID,
                CityName = busSchedule.BusRoute.ToCity.CityName
            };

            dto.Coach = new ScheduleCoachVM
            {
                CoachID = busSchedule.Bus.Coach.CoachID,
                CoachName = busSchedule.Bus.Coach.CoachName
            };

            dto.Bus = new ScheduleBusVM
            {
                BusID = busSchedule.Bus.BusID,
                BusName = busSchedule.Bus.BusName,
                BusNumber = busSchedule.Bus.BusNumber
            };
            return dto;
        }
    }
}
