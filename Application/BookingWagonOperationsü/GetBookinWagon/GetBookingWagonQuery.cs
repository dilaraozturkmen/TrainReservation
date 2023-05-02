using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainBooking.DBOperations;
using TrainBooking.Entiities;
using static TrainBooking.Application.BookingWagonOperationsü.GetBookinWagon.GetBookingWagonQuery;

namespace TrainBooking.Application.BookingWagonOperationsü.GetBookinWagon
{
    public class GetBookingWagonQuery
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;


        public GetBookingWagonQuery(ITrainBookingDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        public List<BookingWagonViewModel> Handle()
        {
            var bookinWagon = _context.BookingWagons.Include(x => x.Wagon).OrderBy(X => X.Id);
            List<BookingWagonViewModel> returnobj = _mapper.Map<List<BookingWagonViewModel>>(bookinWagon);
            return returnobj;
        }
        public class BookingWagonViewModel
        {

            public string Wagon { get; set; }
            public int NumberOfPersons { get; set; }
        }
    }
}
