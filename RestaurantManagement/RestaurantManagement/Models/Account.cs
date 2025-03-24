using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Role { get; set; } = "User";

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual User? User { get; set; }
    }
}
