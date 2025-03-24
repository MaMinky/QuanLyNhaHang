using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Models
{
    public class BookingTable
    {
        [Key]
        public int BookingID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfPeople { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TableNumber { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; } = "Pending";

        public virtual User? User { get; set; }
    }
}
