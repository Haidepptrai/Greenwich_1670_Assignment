using FPT_Pharmacy_Assignment.Areas.Admin.Models;

public class TotalProductViewModel
{
	public int ProductId { get; set; }
	public string? ProductName { get; set; }
	public int QuantitySold { get; set; }
}

public class PopularCategoryViewModel
{
	public int CategoryId { get; set; }
	public string? CategoryName { get; set; }
	public int TotalQuantitySold { get; set; }
}

public class UserActivityViewModel
{
	public string UserId { get; set; }
	public string? UserName { get; set; }
	public int NumberOfOrders { get; set; }
}
public class SalesChartData
{
	public string Month { get; set; }
	public decimal TotalSales { get; set; }
}

public class DashboardViewModel
{
	public decimal TotalSales { get; set; }
	public IEnumerable<TotalProductViewModel> TopSellingProducts { get; set; }
	public IEnumerable<PopularCategoryViewModel> PopularCategories { get; set; }
	public int TotalProductQuantity { get; set; } // Change to int
	public IEnumerable<Order> Orders { get; set; }
	public IEnumerable<SalesChartData> SalesData { get; set; }

}

