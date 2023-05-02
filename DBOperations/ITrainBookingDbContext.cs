using Microsoft.EntityFrameworkCore;
using TrainBooking.Entiities;

namespace TrainBooking.DBOperations
{
    public interface ITrainBookingDbContext
    {
        public DbSet<Train> Trains { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Wagon> Wagons { get; set; }
        public DbSet<BookingWagon> BookingWagons { get; set; }

        int SaveChanges();
    }
}
