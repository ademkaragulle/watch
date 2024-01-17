using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Town { get; set; }
        public string? State { get; set; }
        public string? PostCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime Date { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
