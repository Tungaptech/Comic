using System.ComponentModel.DataAnnotations;

namespace Comic.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public string Status { get; set; }
    }
}