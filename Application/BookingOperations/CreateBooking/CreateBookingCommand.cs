using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainBooking.DBOperations;
using TrainBooking.Entiities;
using TrainBooking.Application.BookingWagonOperationsü.CreateBookinWagon;
using static TrainBooking.Application.BookingWagonOperationsü.GetBookinWagon.GetBookingWagonQuery;
using static TrainBooking.Application.WagonOperations.GetWagon.GetWagonQuery;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainBooking.Application.BookingOperations.CreateBooking
{
    public class CreateBookingCommand
    {
        public readonly IMapper _mapper;
        public readonly ITrainBookingDbContext _context;
        public bool ReservationAvailable;
        public CreateBookingModel Model { get; set; }
        public CreateBookingCommand(ITrainBookingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public JsonResult Handle()
        {
            var train = _context.Trains.Where(x => x.Name == Model.TrainName).FirstOrDefault();
           // rezervasyon yapılabiliniyor mu diye kontrol edilir.
            bool availableWagons = CheckIfBookingIsPossible(train.Id,Model.NumberOfPersonsToReservation,Model.DifferentWagon);
            if (availableWagons == false)
            {
                var result = new
                {
                    ReservationAvailable = false,
                    BookingWagon = ""

                };
                return new JsonResult(result);
            }
            else if (availableWagons == true) 
            {
                if (Model.DifferentWagon == false)
                {   
                    
                    var wagon = GetAvailableWagons(train.Id, Model.NumberOfPersonsToReservation);
                    Booking booking = _mapper.Map<Booking>(Model);
                    booking.TrainId = train.Id;
                    _context.Bookings.Add(booking);  
                    _context.SaveChanges();
                    BookingWagon bookingWagon = AddBookingWagon(booking.Id, wagon.Id, Model.NumberOfPersonsToReservation); 
                    BookingWagonViewModel vm = _mapper.Map<BookingWagonViewModel>(bookingWagon); 
                    var result = new
                    {
                        ReservationAvailable = true,
                        BookingWagon = vm

                    };
                    return new JsonResult(result);                   
                }
                else if (Model.DifferentWagon == true)  
                {
                    int remainingSeat = Model.NumberOfPersonsToReservation;
                    int NumberOfSeat = Model.NumberOfPersonsToReservation;

                    int numberOfseatsToAdd;
                                        Booking booking1 = _mapper.Map<Booking>(Model);
                    booking1.TrainId = train.Id;
                    _context.Bookings.Add(booking1);
                    _context.SaveChanges();
                    // Rezevasyon yapılacak kişileri kapasitesi olan wagonlara ekler.
                    while (NumberOfSeat >= 0)
                    {
                        var wagon = GetAvailableWagons(train.Id, 1); // kapesitesi olan vagonları çekiyor.
                        if (wagon!= null)
                        {
                            var capacity = (int)(wagon.Capacity * 0.7 - wagon.NumberOfFullSeats);
                       
                            numberOfseatsToAdd = (capacity >= remainingSeat) ? remainingSeat : capacity;
                            var bookingWagon = AddBookingWagon(booking1.Id, wagon.Id,numberOfseatsToAdd );
                            remainingSeat = remainingSeat - capacity;
                            NumberOfSeat = remainingSeat; 
                        }
                        else
                        {
                            NumberOfSeat--;
                        }                
                    }
                 
                    var bookingWagonList = _context.BookingWagons.Include(x=>x.Wagon).Where(x=>x.BookingId == booking1.Id).ToList();
                    List<BookingWagonViewModel> vm = _mapper.Map<List<BookingWagonViewModel>>(bookingWagonList);
                    var result = new
                    {
                        ReservationAvailable = true,
                        BookingWagon = vm
                    };
                    return new JsonResult(result);
                }
            }
            return new JsonResult(null);
        }
        private Wagon GetAvailableWagons(int trainId , int NumberOfPersonsToReservation) 
        {
            // tren de kapasitesi dolu olmayan ilk vagonu getirir.
            var wagon = _context.Wagons.FirstOrDefault(x => x.Capacity * 0.7 >= x.NumberOfFullSeats + NumberOfPersonsToReservation && x.TrainId == trainId);
            return wagon;


        }
        private bool CheckIfBookingIsPossible(int trainId, int NumberOfPersonsToReservation,bool differentWagon)
        {
            // REzervasyon yaptırmak isteyen kişi sayısı kadar trenin toplamında yer var mı diye kontrol eder.
            bool result;
            if(differentWagon == true) {
                // Farklı vagonlar kabul ediliyorsa trendeki bütün vagonlardaki bütün boş yerler toplanarak kontrol edilir.
                var emptySeat = 0.0;
                var wagons = _context.Wagons.Where(x => x.Capacity * 0.7 > (double)x.NumberOfFullSeats  && x.TrainId == trainId).ToList();
                foreach (Wagon wagon in wagons)
                {
                    emptySeat = emptySeat + (wagon.Capacity * 0.7) - wagon.NumberOfFullSeats;
                }
                if (emptySeat >= NumberOfPersonsToReservation)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else  
            {
                var wagon = _context.Wagons.FirstOrDefault(x => x.Capacity * 0.7 >= x.NumberOfFullSeats + NumberOfPersonsToReservation && x.TrainId == trainId);
                if(wagon == null)
                {
                    result =false;
                }
                else { result = true; }
            }

            return result;
           
        }
        private BookingWagon AddBookingWagon(int bookingId , int wagonId , int numberOfPerson){

           CreateBookingWagonCommand command = new CreateBookingWagonCommand(_context);
            var result = command.Handle(bookingId,wagonId,numberOfPerson);
            return result;
        }

        public class CreateBookingModel
        {
            public string TrainName { get; set; }
            public List<WagonsViewModel> wagon { get; set; }
            public int NumberOfPersonsToReservation { get; set; }
            public bool DifferentWagon { get; set; }

        }
     

    }
}
