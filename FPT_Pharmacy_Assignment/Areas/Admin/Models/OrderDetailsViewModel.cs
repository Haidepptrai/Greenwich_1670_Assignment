using FPT_Pharmacy_Assignment.Areas.Identity.Data;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Models
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public CustomUser User { get; set; }
    }

}
