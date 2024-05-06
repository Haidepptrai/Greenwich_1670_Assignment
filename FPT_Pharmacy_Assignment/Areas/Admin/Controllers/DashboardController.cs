using FPT_Pharmacy_Assignment.Areas.Admin.Models;
using FPT_Pharmacy_Assignment.Areas.Identity.Data;
using FPT_Pharmacy_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly FPT_Pharmacy_AssignmentContext _context;
        private UserManager<CustomUser> _userManager;

        public DashboardController(FPT_Pharmacy_AssignmentContext context)
        {
            _context = context;
        }

		public async Task<IActionResult> Index()
		{
			// Total Sales
			decimal totalSales = await _context.Order.SumAsync(o => o.TotalPrice);

			// Total Quantity of Products Sold
			var totalQuantitySold = await _context.OrderDetail.SumAsync(od => od.Quantity);

			// Total Product Quantity
			var totalProductQuantity = await _context.Product.SumAsync(p => p.StockLevel);

			// Create a single TotalProductViewModel instance with the total quantity sold
			var totalProduct = new TotalProductViewModel
			{
				QuantitySold = totalQuantitySold
			};

			// Create a list to hold the single TotalProductViewModel instance
			var topSellingProducts = new List<TotalProductViewModel> { totalProduct };

			// Popular Categories
			var popularCategories = await _context.OrderDetail
				.Include(od => od.Product)
				.ThenInclude(p => p.Category)
				.GroupBy(od => od.Product.CategoryId)
				.Select(g => new PopularCategoryViewModel
				{
					CategoryId = g.Key,
					CategoryName = g.FirstOrDefault().Product.Category.Name, // Get the category name
					TotalQuantitySold = g.Sum(od => od.Quantity)
				})
				.OrderByDescending(g => g.TotalQuantitySold)
				.ToListAsync();

			// Orders
			var orders = await _context.Order.ToListAsync(); // Fetch orders directly from the Order table

			// Fetch data for the chart (months and total sales)
			var salesData = await _context.Order
				.GroupBy(o => o.CreatedAt.Month)
				.Select(g => new SalesChartData
				{
					Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
					TotalSales = g.Sum(o => o.TotalPrice)
				})
				.ToListAsync();

			var viewModel = new DashboardViewModel
			{
				TotalSales = totalSales,
				SalesData = salesData,
				TotalProductQuantity = totalProductQuantity,
				TopSellingProducts = topSellingProducts,
				PopularCategories = popularCategories,
				Orders = orders
			};

			return View(viewModel);
		}
	}
}
