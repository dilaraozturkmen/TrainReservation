using System.ComponentModel.DataAnnotations.Schema;

namespace TrainBooking.Entiities
{
    public class BookingWagon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int WagonId { get; set; }
        public Wagon Wagon { get; set; }
        public int NumberOfPersons { get; set; }
    }
}
