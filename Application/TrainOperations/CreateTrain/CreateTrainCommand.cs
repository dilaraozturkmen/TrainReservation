using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainBooking.DBOperations;
using TrainBooking.Entiities;

namespace TrainBooking.Application.TrainOperations.CreateTrain
{
    public class CreateTrainCommand
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;
        public CreateTrainCommand( ITrainBookingDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public int Handle(string trainName)
        {
           Train train= new Train();
            train.Name = trainName;
         
            _context.Trains.Add(train);
            _context.SaveChanges();
            return train.Id;
        }
    
    }
}
