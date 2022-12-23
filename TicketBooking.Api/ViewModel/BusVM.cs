using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Api.ViewModel
{
    public class CityVM
    {
        public Guid CityID { get; set; }
        public string? CityName { get; set; }
        public DateTime UpdateTime { get; set; }
    }
     
    public class AddCityVM
    {
        public string? CityName { get; set; }
    }

    public class CoachVM
    {
        public Guid CoachID { get; set; }
        public string? CoachName { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class BusVM
    {
        public Guid BusID { get; set; }
        public string? BusName { get; set; }
        public string? BusNumber { get; set; }
        public int TotalSeat { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid? CoachID { get; set; }
        public CoachVM Coach { get; set; }
    }

    public class BusScheduleVM
    {
        public Guid ScheduleID { get; set; }
        public Guid? RouteID { get; set; }
        public Guid? BusID { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime UpdateDate { get; set; }
        public  BusVM Bus { get; set; }
        public BusRouteVM BusRoute { get; set; }
    }

    public class BookingDetailsVM
    {
        public Guid BookingID { get; set; }
        public Guid? ScheduleID { get; set; }
        public double TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }
        public string PassengerName { get; set; }
        public string PassengerMobileNo { get; set; }
        public string PassengerEmail { get; set; }
        public DateTime UpdateTime { get; set; }
        public virtual BusScheduleVM? BusScheduleVM { get; set; }
        public ICollection<BookingSeatDetailsVM> BookingSeatDetailsVM { get; set; }
    }
    public class BookingSeatDetailsVM
    {
        public Guid SeatID { get; set; }
        public string SeatNumber { get; set; }
        public DateTime UpdateTime { get; set; }
    }

   

    public class BusScheduleGetVM
    {
        public Guid ScheduleID { get; set; }
        public DateTime? DepartureTime { get; set; }
        public BusGetVM Bus { get; set; }
        public BusRouteGetVM BusRoute { get; set; }
    }

    public class BusGetVM
    {
        public Guid BusID { get; set; }
        public string? BusName { get; set; }
        public string? BusNumber { get; set; }
        public int TotalSeat { get; set; }
        public CoachGetVM Coach { get; set; }
    }

    public class BusRouteGetVM
    {
        public Guid RouteID { get; set; }
        public CityGetVM? FromCity { get; set; }
        public CityGetVM? ToCity { get; set; }
        public CoachGetVM? Coach { get; set; }
        public double Price { get; set; }
    }

    public class CityGetVM
    {
        public Guid CityID { get; set; }
        public string? CityName { get; set; }
    }

    public class CoachGetVM
    {
        public Guid CoachID { get; set; }
        public string? CoachName { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


}


