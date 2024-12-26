namespace Point_Of_Sale.Models
{
    public class ViewOrderModel
    {
        public Orders Orders { get; set; }
        public List<OrderDetails> OrderDetailsList { get; set; }

    }
}
