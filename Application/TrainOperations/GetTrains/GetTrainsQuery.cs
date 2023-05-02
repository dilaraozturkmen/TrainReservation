using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainBooking.DBOperations;
using TrainBooking.Entiities;

namespace TrainBooking.Application.TrainOperations.GetTrains
{
    public class GetTrainsQuery
    {
      
        private readonly ITrainBookingDbContext _context;
        private readonly IMapper _mapper;
        public GetTrainsQuery(ITrainBookingDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public List<TrainsViewModel> Handle()
        {
            var trains = _context.Trains.OrderBy(X => X.Id);
            List<TrainsViewModel> vm = _mapper.Map<List<TrainsViewModel>>(trains);
            return vm;

        }

        public class TrainsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
