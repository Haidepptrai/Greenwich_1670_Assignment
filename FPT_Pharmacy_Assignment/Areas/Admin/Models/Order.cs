using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression(@"^(Pending|Completed|Cancelled)$", ErrorMessage = "Status must be either Pending, Completed, or Cancelled.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Created at date is required.")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        [Required(ErrorMessage = "Total is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total must be greater than or equal to 0.")]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
