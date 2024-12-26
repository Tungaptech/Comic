using System.ComponentModel.DataAnnotations;

namespace Comic.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}