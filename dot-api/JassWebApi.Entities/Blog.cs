namespace JassWebApi.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class BlogSummary
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategorySlug { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
