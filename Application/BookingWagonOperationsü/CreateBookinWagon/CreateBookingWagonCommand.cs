using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainBooking.Application.WagonOperations.UpdateWagon;
using TrainBooking.DBOperations;
using TrainBooking.Entiities;

namespace TrainBooking.Application.BookingWagonOperationsü.CreateBookinWagon
{
    public class CreateBookingWagonCommand
    {
        public readonly ITrainBookingDbContext _context;

        public CreateBookingWagonCommand(ITrainBookingDbContext context)
        {
            _context = context;
        }

        public BookingWagon Handle(int bookingId, int wagonId, int numberOfPersons)
        {

            BookingWagon bookingWagon = new BookingWagon();
            bookingWagon.BookingId = bookingId;
            bookingWagon.WagonId = wagonId;
            bookingWagon.NumberOfPersons = numberOfPersons;
            _context.BookingWagons.Add(bookingWagon);
            _context.SaveChanges();
            // wagon dolu koltuk sayısı güncelleme
            UpdateWagonCommand command = new UpdateWagonCommand(_context);
            command.Handle(wagonId, numberOfPersons);

            return bookingWagon;
        }
    }
}
