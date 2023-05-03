using AutoMapper;
using TrainBooking.DBOperations;

namespace TrainBooking.Application.WagonOperations.GetWagon
{
    public class GetWagonQuery
    {
        public readonly IMapper _mapper;
        public readonly TrainBookingDbContext _context;


        public GetWagonQuery(TrainBookingDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        public List<WagonsViewModel> Handle()
        {
            var wagons = _context.Wagons.OrderBy(X => X.Id);
            List<WagonsViewModel> returnobj = _mapper.Map<List<WagonsViewModel>>(wagons);
            return returnobj;
        }
        public class WagonsViewModel
        {
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int NumberOfFullSeats { get; set; }
         
        }
    }

 }

