using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Image")]
        public string ImageFile { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }  // Navigation property

        [StringLength(650)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock level is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock level must be a positive number.")]
        public int StockLevel { get; set; }
        public Category? Category { get; set; }
    }
}
