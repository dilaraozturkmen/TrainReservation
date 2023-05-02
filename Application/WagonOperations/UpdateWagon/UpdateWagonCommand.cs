using AutoMapper;
using static TrainBooking.Application.BookingOperations.CreateBooking.CreateBookingCommand;
using static TrainBooking.Application.WagonOperations.CreateWagon.CreateWagonCommand;
using TrainBooking.DBOperations;
using TrainBooking.Entiities;

namespace TrainBooking.Application.WagonOperations.UpdateWagon
{
    public class UpdateWagonCommand
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;

        public UpdateWagonCommand(ITrainBookingDbContext context)
        {
            _context = context;
        }

        public void Handle(int wagonId, int numberOfPerson)
        {
            var wagon = _context.Wagons.First(x=>x.Id == wagonId);
            wagon.NumberOfFullSeats = wagon.NumberOfFullSeats + numberOfPerson;
            _context.Wagons.Update(wagon);
            _context.SaveChanges();

        }
     
    }
}
