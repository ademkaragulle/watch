namespace API.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Author { get; set; }
        public DateTime Date { get; set; }
        public string? ShortContent { get; set; }
        public string? LongContent { get; set; }
        public string? Image { get; set; }

    }
}
