using System.ComponentModel.DataAnnotations;

namespace Comic.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailId { get; set; }

        public int RentalId { get; set; }
        public Rental Rental { get; set; }

        public int ComicBookId { get; set; }
        public ComicBook ComicBook { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }
    }
}