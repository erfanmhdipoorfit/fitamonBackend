namespace Fitamon.Domain.Blog.Entities
{
    public class BlogEntity
    {
        public int Id { get; set; }          // ✅ EF Core به‌صورت خودکار این رو Primary Key می‌دونه
        public string Name { get; set; } = string.Empty;
    }
}
