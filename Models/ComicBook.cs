using System.ComponentModel.DataAnnotations;

namespace Comic.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}