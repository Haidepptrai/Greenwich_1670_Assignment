using FPT_Pharmacy_Assignment.Areas.Identity.Data;

namespace FPT_Pharmacy_Assignment.Models
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public CustomUser User { get; set; }
    }
}
