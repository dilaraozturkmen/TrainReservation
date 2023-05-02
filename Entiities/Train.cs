using System.ComponentModel.DataAnnotations.Schema;

namespace TrainBooking.Entiities
{
    public class Train
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
   
  
    }
}
