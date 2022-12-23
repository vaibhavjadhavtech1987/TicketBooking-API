using Microsoft.AspNetCore.Mvc;
using TicketBooking.Api.Entities;
using TicketBooking.Api.Repository;
using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoachController : Controller
    {
        private readonly ICoachRepository _coachRepository;
        public CoachController(ICoachRepository coachRepository)
        {
            this._coachRepository = coachRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoachesAsync()
        {
            var coaches = await this._coachRepository.GetAllAsync();
            var coachVMs = new List<CoachVM>();
            if (coaches != null)
            {
                foreach (var item in coaches)
                {
                    var _coach = MapEntity(item);
                    coachVMs.Add(_coach);
                }
            }
            return Ok(coachVMs);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCoachAsync")]
        public async Task<IActionResult> GetCoachAsync(Guid id)
        {
            var coach = await this._coachRepository.GetAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            var _coachVM = MapEntity(coach);
            return Ok(_coachVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoachAsync(CoachVM coachddto)
        {
            var _coach = new Coach { CoachName = coachddto.CoachName };
            var _coachresult = await this._coachRepository.AddAsync(_coach);
            var _coachDto = new CoachVM { CoachID = _coachresult.CoachID, CoachName = _coachresult.CoachName };
            return Ok(_coachDto);
        }

        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCoachAsync(Guid id)
        {
            var _coachresult = await this._coachRepository.DeleteAsync(id);
            var _coachDto = new CoachVM { CoachID = _coachresult.CoachID, CoachName = _coachresult.CoachName };
            return Ok(_coachDto);
        }

        [HttpPost]
        [Route("Update/{id:guid}")]
        public async Task<IActionResult> UpdateCoachAsync([FromRoute] Guid id, [FromBody] CoachVM coachDto)
        {
            Coach coach = new Coach { CoachID = id, CoachName = coachDto.CoachName };
            var _coachresult = await this._coachRepository.UpdateAsync(id, coach);
            var _coachDto = new CoachVM { CoachID = _coachresult.CoachID, CoachName = _coachresult.CoachName };
            return Ok(_coachDto);
        }

        private CoachVM MapEntity(Coach coach)
        { 
            return  new CoachVM { CoachID = coach.CoachID, CoachName = coach.CoachName, UpdateTime = coach.UpdateTime };

        }
    }
}
