namespace API.Models
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public List<ProductViewModel> OrderList { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
