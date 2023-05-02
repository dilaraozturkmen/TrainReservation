using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainBooking.Application.BookingWagonOperationsü.GetBookinWagon;
using TrainBooking.Application.TrainOperations.GetTrains;
using TrainBooking.Application.WagonOperations.GetWagon;
using TrainBooking.DBOperations;

namespace TrainBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagonController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;

        public WagonController(ITrainBookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetWagons()
        {
            GetWagonQuery query = new GetWagonQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);

        }

    }
}
