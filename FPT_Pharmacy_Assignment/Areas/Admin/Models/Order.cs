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

        [Required(ErrorMessage = "Updated at date is required.")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        public OrderDetail OrderDetail { get; set; }
    }
}
