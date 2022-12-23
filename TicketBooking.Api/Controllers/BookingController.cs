using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Api.Entities;
using TicketBooking.Api.Repository;
using TicketBooking.Api.ViewModel;

namespace TicketBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            this._bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var listBookingDetailsGetVM = new List<BookingDetailsGetVM>();
            var bookingDetails = await this._bookingRepository.GetAllAsync();
            if (bookingDetails != null)
            {
                foreach (var _booking in bookingDetails)
                {
                    if (_booking != null)
                    {
                        var bookingDetailsGetVM = new BookingDetailsGetVM();
                        bookingDetailsGetVM = MapEntity(_booking);
                        if (bookingDetailsGetVM != null)
                        {
                            listBookingDetailsGetVM.Add(bookingDetailsGetVM);
                        }
                    }
                }
            }
            return Ok(listBookingDetailsGetVM);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var bookingDetailsGetVM = new BookingDetailsGetVM();
            var _booking = await this._bookingRepository.GetAsync(id);
            if (_booking != null)
            {
                bookingDetailsGetVM = MapEntity(_booking);
            }
            return Ok(bookingDetailsGetVM);
        }

        [HttpGet]
        [Route("Schedules/{id:guid}")]
        public async Task<IActionResult> GetByScheduleIDAsync(Guid id)
        {
            var bookingDetailsGetVM = new BookingDetailsGetVM();
            var bookingDetails = await this._bookingRepository.GetByScheduleIDAsync(id);
            if (bookingDetails != null)
            {
                foreach (var _booking in bookingDetails)
                {
                    if (_booking != null)
                    {
                        bookingDetailsGetVM = MapEntity(_booking);
                    }
                }
            }
            return Ok(bookingDetailsGetVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] BookingDetailsAddVM bookingDetails)
        {
           var newbookingDetails =  MapEntity(bookingDetails);
            var _bookingresult = this._bookingRepository.Add(newbookingDetails);

            if (_bookingresult == null)
            {
                return BadRequest("routes not configured, for given inputs");
            }

            var _bookingID = _bookingresult.BookingID;
            foreach (var item in bookingDetails.Seats)
            {
                BookingSeatDetails newbookingSeatDetails = new BookingSeatDetails();
                newbookingSeatDetails.BookingID = _bookingID;
                newbookingSeatDetails.SeatNumber = item.SeatNumber;
                newbookingSeatDetails.UpdateTime = DateTime.Now;
                var _scheduleresult = await this._bookingRepository.AddSeatAsync(newbookingSeatDetails);
            }
            return Ok(_bookingresult);
        }


        private BookingDetailsGetVM MapEntity(BookingDetails _booking)
        {
            var bookingDetailsGetVM = new BookingDetailsGetVM();
            if (_booking != null)
            {
                bookingDetailsGetVM.BookingID = _booking.BookingID;
                bookingDetailsGetVM.ScheduleID = _booking.ScheduleID;
                bookingDetailsGetVM.TotalAmount = _booking.TotalAmount;
                bookingDetailsGetVM.BookingDate = _booking.BookingDate;
                bookingDetailsGetVM.PassengerName = _booking.PassengerName;
                bookingDetailsGetVM.PassengerMobileNo = _booking.PassengerMobileNo;
                bookingDetailsGetVM.PassengerEmail = _booking.PassengerEmail;
                bookingDetailsGetVM.DepartureTime = _booking?.BusSchedule?.DepartureTime;
                if (_booking?.BusSchedule?.Bus != null)
                {
                    bookingDetailsGetVM.BusName = _booking?.BusSchedule?.Bus?.BusName;
                    bookingDetailsGetVM.BusNumber = _booking?.BusSchedule?.Bus?.BusNumber;
                    bookingDetailsGetVM.TotalSeat = _booking.BusSchedule.Bus.TotalSeat;
                    bookingDetailsGetVM.CoachName = _booking?.BusSchedule?.BusRoute?.Coach?.CoachName;
                }
                if (_booking.BookingSeatDetails != null)
                {
                    foreach (var item in _booking.BookingSeatDetails)
                    {
                        var _seat = new SeatDetailsVM { SeatNumber = item.SeatNumber };
                        bookingDetailsGetVM.Seats.Add(_seat);
                    }
                }
            }
            return bookingDetailsGetVM;
        }
        private BookingDetails MapEntity(BookingDetailsAddVM bookingDetails)
        {
            BookingDetails newbookingDetails = new BookingDetails();
            newbookingDetails.ScheduleID = bookingDetails.ScheduleID;
            newbookingDetails.TotalAmount = bookingDetails.TotalAmount;
            newbookingDetails.BookingDate = bookingDetails.BookingDate;
            newbookingDetails.PassengerName = bookingDetails.PassengerName;
            newbookingDetails.PassengerMobileNo = bookingDetails.PassengerMobileNo;
            newbookingDetails.PassengerEmail = bookingDetails.PassengerEmail;
            newbookingDetails.UpdateTime = DateTime.Now;
            return newbookingDetails;
        }
    }
}
