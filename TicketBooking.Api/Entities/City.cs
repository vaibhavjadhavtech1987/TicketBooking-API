using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking.Api.Entities
{
    public class City
    {
        [Key]
        public Guid CityID { get; set; }
        public string? CityName { get; set; }
        public DateTime UpdateTime { get; set; }

        public ICollection<BusRoute> FromBusRoutes { get; set; }
        public ICollection<BusRoute> ToBusRoutes { get; set; }
    }

    public class Coach
    {
        [Key]
        public Guid CoachID { get; set; }
        public string? CoachName { get; set; }
        public DateTime UpdateTime { get; set; }

        public ICollection<Bus> Buses { get; set; }
        public ICollection<BusRoute> BusRoutes { get; set; }
    }
   
    public class Bus
    {
        [Key]
        public Guid BusID { get; set; }
        public string? BusName { get; set; }
        public string? BusNumber { get; set; }
        public int TotalSeat { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid? CoachID { get; set; }
        public virtual Coach Coach { get; set; }
        public ICollection<BusSchedule> BusSchedules { get; set; }
    }

    public class BusRoute
    {
        [Key]
        public Guid RouteID { get; set; }
        public Guid? FromCityID { get; set; }
        public City? FromCity { get; set; }
        public Guid? ToCityID { get; set; }
        public City? ToCity { get; set; }
        public Guid? CoachID { get; set; }
        public Coach? Coach { get; set; }
        public double Price { get; set; }
        public DateTime UpdateTime { get; set; }
        public ICollection<BusSchedule> BusSchedules { get; set; }
    }

    public class BusSchedule
    {
        [Key]
        public Guid ScheduleID { get; set; }
        public Guid? RouteID { get; set; }
        public Guid? BusID { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual Bus Bus { get; set; }
        public virtual BusRoute BusRoute { get; set; }

        public ICollection<BookingDetails> BookingDetails { get; set; }
    }

    public class BookingDetails
    {
        [Key]
        public Guid BookingID { get; set; }
        public Guid? ScheduleID { get; set; }
        public double TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }
        public string PassengerName { get; set; }
        public string PassengerMobileNo { get; set; }
        public string PassengerEmail { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual BusSchedule? BusSchedule { get; set; }

        public ICollection<BookingSeatDetails> BookingSeatDetails { get; set; }
    }

    public class BookingSeatDetails
    {
        [Key]
        public Guid SeatID { get; set; }
        public Guid? BookingID { get; set; }
        public string SeatNumber { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual BookingDetails BookingDetails { get; set; }
    }
    public class User
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }
        public List<User_Role> UserRoles { get; set; }
    }

    public class Role
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<User_Role> UserRoles { get; set; }
    }

    public class User_Role
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public Guid RoleID { get; set; }
        public Role Role { get; set; }
    }
}
