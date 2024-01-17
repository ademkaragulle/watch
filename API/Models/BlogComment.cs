namespace API.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public int BlogId { get; set; }
        public DateTime Date { get; set; }
    }
}
