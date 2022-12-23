using Microsoft.EntityFrameworkCore;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface IUserRepository
    {
        Task<User> AutheticateUserAsync(string useername, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly TicketBookingDataContext _dataContext;
        public UserRepository(TicketBookingDataContext dataContext)
        {
            this._dataContext = dataContext;

        }

        public async Task<User> AutheticateUserAsync(string username, string password)
        {
            var user = await this._dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);

            if (user == null)
                return null;

            var userRoles = await this._dataContext.User_Roles.Where(x => x.UserID == user.UserID).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await this._dataContext.Roles.FirstOrDefaultAsync(x => x.ID == userRole.RoleID);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            return user;
        }
    }
}
