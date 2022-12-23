namespace TicketBooking.Api.ViewModel
{
    public class BookingDetailsGetVM
    {
        public Guid BookingID { get; set; }
        public Guid? ScheduleID { get; set; }
        public double TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }
        public string PassengerName { get; set; }
        public string PassengerMobileNo { get; set; }
        public string PassengerEmail { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string? BusName { get; set; }
        public string? BusNumber { get; set; }
        public int TotalSeat { get; set; }
        public BusGetVM BusGetVM { get; set; }
        public string? CoachName { get; set; }
        public ICollection<SeatDetailsVM> Seats { get; set; }
    }

    public class SeatDetailsVM
    {
        public string SeatNumber { get; set; }
    }

    public class BookingDetailsAddVM
    {
        public Guid ScheduleID { get; set; }
        public double TotalAmount { get; set; }
        public DateTime BookingDate { get; set; }
        public string PassengerName { get; set; }
        public string PassengerMobileNo { get; set; }
        public string PassengerEmail { get; set; }
        public IEnumerable<SeatDetailsVM> Seats { get; set; }
    }
}
