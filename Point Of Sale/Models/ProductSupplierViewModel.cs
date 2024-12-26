namespace Point_Of_Sale.Models
{
    public class ProductSupplierViewModel
    {
        public Products? Product { get; set; }
        public ProductSupplier? ProductSupplier { get; set; }

        public ProductCategories? ProductCategories { get; set; }

        public Suppliers? Suppliers { get; set; }
    }
}
