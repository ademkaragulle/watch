
namespace API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public int NewPrice { get; set; }
        public int OldPrice { get; set; }
        public string? Gender { get; set; }

       
    }
}
