using Microsoft.EntityFrameworkCore;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.Repository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingDetails>> GetAllAsync();
        Task<BookingDetails> GetAsync(Guid guid);
        Task<IEnumerable<BookingDetails>> GetByScheduleIDAsync(Guid guid);
        BookingDetails Add(BookingDetails bookingDetails);
        Task<BookingSeatDetails> AddSeatAsync(BookingSeatDetails bookingDetails);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly TicketBookingDataContext _dataContext;


        public BookingRepository(TicketBookingDataContext dataContext)
        {
            this._dataContext = dataContext;

        }

        public async Task<IEnumerable<BookingDetails>> GetAllAsync()
        {
            return await _dataContext.BookingDetails
                    .Include(x => x.BusSchedule)
                    .Include(x => x.BusSchedule.Bus)
                    .Include(x => x.BusSchedule.Bus.Coach)
                    .Include(x => x.BusSchedule.BusRoute)
                    .Include(x => x.BusSchedule.BusRoute.FromCity)
                    .Include(x => x.BusSchedule.BusRoute.ToCity)
                    .ToListAsync();
        }

        public async Task<BookingDetails> GetAsync(Guid guid)
        {
            var bookingDetails = await _dataContext.BookingDetails
                                .Include(x => x.BusSchedule)
                                .Include(x => x.BusSchedule.Bus)
                                .Include(x => x.BusSchedule.Bus.Coach)
                                .Include(x => x.BusSchedule.BusRoute)
                                .Include(x => x.BusSchedule.BusRoute.FromCity)
                                .Include(x => x.BusSchedule.BusRoute.ToCity).FirstOrDefaultAsync(x => x.BookingID == guid);
            return bookingDetails;
        }

        public async Task<IEnumerable<BookingDetails>> GetByScheduleIDAsync(Guid guid)
        {

             var bookingDetails = await _dataContext.BookingDetails
                                .Include(x => x.BusSchedule.ScheduleID.Equals(guid))
                                .Include(x => x.BusSchedule.Bus)
                                .Include(x => x.BusSchedule.Bus.Coach)
                                .Include(x => x.BusSchedule.BusRoute)
                                .Include(x => x.BusSchedule.BusRoute.FromCity)
                                .Include(x => x.BusSchedule.BusRoute.ToCity).ToListAsync(); ;
            return bookingDetails;
        }


        public BookingDetails Add(BookingDetails bookingDetails)
        {
            bookingDetails.BookingID = new Guid();
            _dataContext.Add(bookingDetails);
            _dataContext.SaveChanges();
            return bookingDetails;
        }

        public async Task<BookingSeatDetails> AddSeatAsync(BookingSeatDetails bookingDetails)
        {
            bookingDetails.SeatID = new Guid();
            await _dataContext.AddAsync(bookingDetails);
            await _dataContext.SaveChangesAsync();
            return bookingDetails;
        }

    }
}
