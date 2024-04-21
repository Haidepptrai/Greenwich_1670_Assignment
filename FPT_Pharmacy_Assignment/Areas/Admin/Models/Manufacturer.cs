    using FPT_Pharmacy_Assignment.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

public class Manufacturer
{
    [Key]
    public int ManufacturerId { get; set; }

    [Required(ErrorMessage = "Manufacturer name is required.")]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Manufacturer name should not contain numbers or special characters.")]
    public string Name { get; set; }

    public string? Address { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^(?:\+\d{1,3})?\d{9,12}$", ErrorMessage = "Invalid phone number.")]
    public string? Phone { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
}
