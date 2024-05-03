using FPT_Pharmacy_Assignment.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public int CartItemId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
    public string ProductName { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [NotMapped]
    public decimal TotalPrice => Price * Quantity;

    public virtual Product Product { get; set; }
}
