using System.ComponentModel.DataAnnotations.Schema;

namespace TrainBooking.Entiities
{
    public class Wagon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int NumberOfFullSeats { get; set; }
        public int TrainId { get; set; }
        public Train Train { get; set; }
    }
}

