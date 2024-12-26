using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ReportsViewModel rvm = new ReportsViewModel();

            List<SalesReportItem> sri = new List<SalesReportItem>();
            List<ProductReportItem> pri = new List<ProductReportItem>();

            var OrderDetails = await _context.OrderDetails.ToListAsync();

            var productNames = await _context.Products
                .Where(p => OrderDetails.Select(od => od.ProductID).Contains(p.ProductID))
                .ToDictionaryAsync(p => p.ProductID, p => p.ProductName);

            var orderDates = await _context.Orders
                .Where(o => OrderDetails.Select(od => od.OrderID).Contains(o.OrderID))
                .ToDictionaryAsync(o => o.OrderID, o => o.OrderDate);

            foreach (var item in OrderDetails)
            {
                var itemSalesReport = new SalesReportItem
                {
                    SaleID = item.OrderDetailID,
                    ProductName = productNames.ContainsKey(item.ProductID) ? productNames[item.ProductID] : "Unknown",
                    Quantity = item.Quantity,
                    TotalAmount = item.TotalPrice,
                    SaleDate = orderDates.ContainsKey(item.OrderID) ? orderDates[item.OrderID] : ""
                };

                sri.Add(itemSalesReport);
            }

            var ProductSupplier = await _context.ProductSupplier.ToListAsync();

            // Fetch all products and categories at once using dictionaries for fast lookup.
            var products = await _context.Products
                .Where(p => ProductSupplier.Select(ps => ps.ProductID).Contains(p.ProductID))
                .ToDictionaryAsync(p => p.ProductID, p => p.ProductName);

            var Products = await _context.Products.ToListAsync();

            var categories = await _context.Products
                 .Join(
                    _context.ProductCategories,
                    product => product.CategoryID,
                    category => category.CategoryID,
                    (product, category) => new { product.ProductID, category.CategoryName }
                )
                .ToDictionaryAsync(pc => pc.ProductID, pc => pc.CategoryName);


            // Loop through the ProductSupplier and populate the ProductReportItem list.
            foreach (var item in ProductSupplier)
            {
                var itemProductReport = new ProductReportItem
                {
                    ProductID = item.ProductID,
                    ProductName = products.ContainsKey(item.ProductID) ? products[item.ProductID] : "Unknown", // Use dictionary for fast lookup.
                    Category = categories.ContainsKey(item.ProductID) ? categories[item.ProductID] : "Unknown", // Use dictionary for fast lookup.
                    Price = item.Price,
                    StockQuantity = item.QuantityInStock
                };

                pri.Add(itemProductReport);
            }

            rvm.ProductReport = pri;
            rvm.SalesReport = sri;

            return View(rvm);
        }
    }
}
