using System.ComponentModel.DataAnnotations.Schema;

namespace TrainBooking.Entiities
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Train train { get; set; }
        public int TrainId { get; set; }
        public int NumberOfPersonsToReservation { get; set; }
        public bool DifferentWagon { get; set; }
    }
}
