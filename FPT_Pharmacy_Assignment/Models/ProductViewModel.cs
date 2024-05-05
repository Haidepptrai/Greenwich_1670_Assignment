using FPT_Pharmacy_Assignment.Areas.Admin.Models;

namespace FPT_Pharmacy_Assignment.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string searchByName { get; set; }

		public ProductViewModel(IEnumerable<Product> products, IEnumerable<Category> categories)
        {
            Products = products;
            Categories = categories;
        }
        public ProductViewModel()
        {
            Products = new List<Product>();
            Categories = new List<Category>();
        }
        public ProductViewModel(IEnumerable<Product> products)
        {
            Products = products;
        }
    }
}
