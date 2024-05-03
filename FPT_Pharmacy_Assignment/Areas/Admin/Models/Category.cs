using System.ComponentModel.DataAnnotations;
using FPT_Pharmacy_Assignment.Areas.Admin.Models;

public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
}