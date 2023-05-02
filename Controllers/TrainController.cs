using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainBooking.Application.TrainOperations.GetTrains;
using TrainBooking.DBOperations;

namespace TrainBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;

        public TrainController(ITrainBookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTrains()
        {
            GetTrainsQuery query = new GetTrainsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);

        }
    }
}
