using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TicketBooking.Api.Entities;
using TicketBooking.Api.Repository;
using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusController : Controller
    {
        private readonly IBusRepository _busRepository;

        public BusController(IBusRepository busRepository)
        {
            this._busRepository = busRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBuses()
        {
            var buses = await this._busRepository.GetAllAsync();
            List<BusVM> busVMs = new List<BusVM>();
            if (buses != null)
            {
                foreach (var item in buses)
                {
                    var _busVM = MapEntity(item);
                    busVMs.Add(_busVM);
                }
            }
            return Ok(busVMs);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(BusAddVM bus)
        {
            var _bus = MapEntity(bus);
            var _busresult = await this._busRepository.AddAsync(_bus);
            var _busVM = MapEntity(_busresult);
            return Ok(_busVM);
        }

        private BusVM MapEntity(Bus _busresult)
        {
            return new BusVM
            {
                BusID = _busresult.BusID,
                BusName = _busresult.BusName,
                BusNumber = _busresult.BusNumber,
                CoachID = _busresult.CoachID,
                TotalSeat = _busresult.TotalSeat,
                UpdateTime = _busresult.UpdateTime,
                Coach = ((_busresult?.Coach != null) ? new CoachVM { CoachID = _busresult.Coach.CoachID, CoachName = _busresult.Coach.CoachName } : null)
            };
        }

        private Bus MapEntity(BusAddVM bus)
        {
            return new Bus
            {
                BusName = bus.BusName,
                BusNumber = bus.BusNumber,
                CoachID = bus.CoachID,
                TotalSeat = bus.TotalSeat,
                UpdateTime = bus.UpdateTime,
            };
        }
    }
}
