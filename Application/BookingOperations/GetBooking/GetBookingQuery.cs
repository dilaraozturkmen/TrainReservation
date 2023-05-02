using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainBooking.DBOperations;

namespace TrainBooking.Application.BookingOperations.GetBooking
{
    public class GetBookingQuery
    {

        private readonly ITrainBookingDbContext _context;
        private readonly IMapper _mapper;
        public GetBookingQuery(ITrainBookingDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public List<BookingsViewModel> Handle()
        {
            var bookings = _context.Bookings.Include(x => x.train).OrderBy(x => x.Id).ToList();
            List <BookingsViewModel> vm = _mapper.Map<List<BookingsViewModel>>(bookings);
            return vm;

        }

        public class BookingsViewModel
        {
            public int Id { get; set; }
            public string Train { get; set; }
            public int NumberOfPersonsToReservation { get; set; }
            public bool DifferentWagon { get; set; }
        }
    }
}
