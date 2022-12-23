using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Api.Entities;
using TicketBooking.Api.Repository;
using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            this._cityRepository = cityRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllCitiesAsync()
        {
            var cities = await this._cityRepository.GetAllAsync();
            var cityDtos = new List<CityVM>();
            if (cities != null)
            {
                foreach (var city in cities)
                {
                    var _cityVM = MapEntity(city);
                    cityDtos.Add(_cityVM);
                }
            }
            return Ok(cityDtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCityAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetCityAsync(Guid id)
        {
            var city = await this._cityRepository.GetAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            var _cityVM = MapEntity(city);
            return Ok(_cityVM);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "writter")]
        public async Task<IActionResult> AddCityAsync(AddCityVM city)
        {
            //if (!validateAddCityAsync(city))
            //    return BadRequest();

            var _city = new City { CityName = city.CityName };
            var _cityresult = await this._cityRepository.AddAsync(_city);
            var _cityDto = new CityVM { CityID = _cityresult.CityID, CityName = _cityresult.CityName };
            return Ok(_cityDto);
        }

        [HttpPost]
        [Route("{id:guid}")]
        [Authorize(Roles = "writter")]
        public async Task<IActionResult> DeleteCityAsync(Guid id)
        {
            var _cityresult = await this._cityRepository.DeleteAsync(id);
            var _cityDto = new CityVM { CityID = _cityresult.CityID, CityName = _cityresult.CityName };
            return Ok(_cityDto);
        }

        [HttpPost]
        [Route("Update/{id:guid}")]
        [Authorize(Roles = "writter")]
        public async Task<IActionResult> UpdateCityAsync([FromRoute] Guid id, [FromBody] CityVM cityDto)
        {
            City city = new City { CityID = id, CityName = cityDto.CityName };
            var _cityresult = await this._cityRepository.UpdateAsync(id, city);
            var _cityDto = new CityVM { CityID = _cityresult.CityID, CityName = _cityresult.CityName };
            return Ok(_cityDto);
        }


        private bool validateAddCityAsync(AddCityVM city)
        {
            if (string.IsNullOrWhiteSpace(city.CityName))
            {
                ModelState.AddModelError(nameof(city.CityName), $"{nameof(city.CityName)} can not be blank.");
                return false;
            }
            return true;
        }

        private CityVM MapEntity(City city)
        {
            return new CityVM { CityID = city.CityID, CityName = city.CityName, UpdateTime = city.UpdateTime };

        }
    }
}
