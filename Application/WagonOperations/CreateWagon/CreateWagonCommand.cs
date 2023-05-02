using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using TrainBooking.DBOperations;
using TrainBooking.Entiities;
using static TrainBooking.Application.BookingOperations.CreateBooking.CreateBookingCommand;
using static TrainBooking.Application.WagonOperations.GetWagon.GetWagonQuery;

namespace TrainBooking.Application.WagonOperations.CreateWagon
{
    public class CreateWagonCommand
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;

        public CreateWagonCommand(ITrainBookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CreateWagonModel Model { get; set; }
        public void Handle (WagonsViewModel wagon, int trainId)
        {
            Wagon _wagon = _mapper.Map<Wagon>(wagon);
            _wagon.TrainId = trainId;
            _context.Wagons.Add(_wagon);
            _context.SaveChanges();

        }

 
        public class CreateWagonModel
        {
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int NumberOfFullSeats { get; set; }
            public int TrainId { get; set; }

        }
    }
}
