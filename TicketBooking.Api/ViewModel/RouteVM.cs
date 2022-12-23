using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.ViewModel
{
    public class BusRouteVM
    {
        public Guid RouteID { get; set; }
        public double Price { get; set; }
        public DateTime UpdateTime { get; set; }
        public ScheduleCityVM? FromCity { get; set; }
        public ScheduleCityVM? ToCity { get; set; }
        public ScheduleCoachVM? Coach { get; set; }
    }

    public class BusRouteAddVM
    {
        public Guid RouteID { get; set; }
        public double Price { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid FromCityID { get; set; }
        public Guid ToCityID { get; set; }
        public Guid CoachID { get; set; }
    }


    public class BusAddVM
    {
        public Guid BusID { get; set; }
        public string? BusName { get; set; }
        public string? BusNumber { get; set; }
        public int TotalSeat { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid? CoachID { get; set; }
    }
}