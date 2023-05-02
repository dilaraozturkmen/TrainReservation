using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using TrainBooking.Application.BookingOperations.CreateBooking;
using TrainBooking.Application.BookingOperations.GetBooking;
using TrainBooking.Application.TrainOperations.CreateTrain;
using TrainBooking.Application.WagonOperations.CreateWagon;
using TrainBooking.DBOperations;
using static TrainBooking.Application.BookingOperations.CreateBooking.CreateBookingCommand;

namespace TrainBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;

        public BookingController(ITrainBookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBookings()
        {
            GetBookingQuery query = new GetBookingQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);

        }
        [HttpPost]
        public IActionResult AddBooking([FromBody] CreateBookingModel newBooking)
        {
            
            CreateTrainCommand createTrain = new CreateTrainCommand(_context, _mapper);
            var trainId =createTrain.Handle(newBooking.TrainName);
            CreateWagonCommand createWagon = new CreateWagonCommand(_context, _mapper);
            foreach (var wagon in newBooking.wagon)  // Birden fazla vagon için döngü oluşturulur
            {
                createWagon.Handle(wagon, trainId);
            }
           
            CreateBookingCommand command = new CreateBookingCommand(_context, _mapper);

            command.Model = newBooking;

            CreateBookingCommandValidator validator = new CreateBookingCommandValidator();
            ValidationResult result = validator.Validate(command);

            validator.ValidateAndThrow(command);

            var result1 = command.Handle();

            return new JsonResult(result1);

        }

    }
}
