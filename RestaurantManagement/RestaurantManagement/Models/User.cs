using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(100)]
        public required string FullName { get; set; } // Thêm required

        [Required, StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; } // Thêm required

        public virtual Account? Account { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<BookingTable>? Bookings { get; set; }
    }
}