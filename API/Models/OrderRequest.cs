namespace API.Models
{
    public class OrderRequest : Order
    {
        public List<ProductRequest> Orders { get; set; }
        public string Username { get; set; }
    }
}
