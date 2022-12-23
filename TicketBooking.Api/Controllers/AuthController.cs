using Microsoft.AspNetCore.Mvc;
using TicketBooking.Api.Repository;
using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenhandler _tokenhandler;

        public AuthController(IUserRepository userRepository, ITokenhandler tokenhandler)
        {
            _userRepository = userRepository;
            _tokenhandler = tokenhandler;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.AutheticateUserAsync(loginRequest.Username, loginRequest.Password);
            if (user != null)
            {
                var token = await _tokenhandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("UserName or Password is incorrect");
        }
    }
}
