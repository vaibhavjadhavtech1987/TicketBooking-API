namespace TicketBooking.Api.ViewModel
{
    public class ScheduleAddVM
    {
        public Guid FromCityID { get; set; }
        public Guid ToCityID { get; set; }
        public Guid BusID { get; set; }
        public Guid CoachID { get; set; }
        public DateTime DepartureTime { get; set; }
    }

    public class ScheduleGetVM
    {
        public Guid? ScheduleID { get; set; }
        public DateTime? DepartureTime { get; set; }
        public int? TotalSeat { get; set; }
        public int ReserveSeats { get; set; }
        public double Price { get; set; }
        public ScheduleCityVM? FromCity { get; set; }
        public ScheduleCityVM? ToCity { get; set; }
        public ScheduleBusVM? Bus { get; set; }
        public ScheduleCoachVM? Coach { get; set; }
    }
     public class ScheduleBusVM
    {
        public Guid BusID { get; set; }
        public string? BusName { get; set; }
        public string? BusNumber { get; set; }
    }

    public class ScheduleCityVM
    {
        public Guid CityID { get; set; }
        public string? CityName { get; set; }
    }

    public class ScheduleCoachVM
    {
        public Guid CoachID { get; set; }
        public string? CoachName { get; set; }
    }
}
