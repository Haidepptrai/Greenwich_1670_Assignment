using FPT_Pharmacy_Assignment.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Pharmacy_Assignment.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            if (cart == null)
            {
                cart = new List<CartItem>(); // Ensure there is a list even if it's empty
            }
            return View(cart);
        }
    }
}
