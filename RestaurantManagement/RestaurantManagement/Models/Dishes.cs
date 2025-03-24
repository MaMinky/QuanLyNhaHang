using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Models
{
    public class Dishes
    {
        [Key]
        public int DishID { get; set; }

        [Required, StringLength(100)]
        public string DishName { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required, StringLength(50)]
        public string Category { get; set; } = "Other";

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
