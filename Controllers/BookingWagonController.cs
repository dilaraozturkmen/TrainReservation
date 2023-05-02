using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainBooking.Application.BookingWagonOperationsü.GetBookinWagon;
using TrainBooking.DBOperations;

namespace TrainBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingWagonController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;

        public BookingWagonController(ITrainBookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GeBookingWagons()
        {
            GetBookingWagonQuery query = new GetBookingWagonQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);

        }
    }
}
