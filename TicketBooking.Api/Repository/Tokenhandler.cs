using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface ITokenhandler
    {
        Task<string> CreateTokenAsync(User user);
    }
    public class Tokenhandler : ITokenhandler
    {
        private readonly IConfiguration _configuration;

        public Tokenhandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));

            user.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credntials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(300),
                    signingCredentials: credntials
                );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
