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
                cart = new List<CartItem>();
            }
            return View(cart);
        }

        [HttpGet]
        public IActionResult RemoveItem(int id)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var itemToRemove = cart.SingleOrDefault(item => item.ProductId == id);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }
    }
}
