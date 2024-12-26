namespace Point_Of_Sale.Models
{
    public class ReportsViewModel
    {
        public IEnumerable<SalesReportItem>? SalesReport { get; set; }
        public IEnumerable<ProductReportItem>? ProductReport { get; set; }
    }

    public class SalesReportItem
    {
        public string? SaleID { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? TotalAmount { get; set; }
        public string? SaleDate { get; set; }
    }

    public class ProductReportItem
    {
        public string? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public int? Price { get; set; }
        public int? StockQuantity { get; set; }
    }

}
