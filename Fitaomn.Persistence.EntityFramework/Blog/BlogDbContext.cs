using Fitamon.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    public DbSet<BlogEntity> Blogs { get; set; } // بهتره اسمش جمع باشه: Blogs

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ✅ درست: روی BlogEntity تنظیم کنید
        modelBuilder.Entity<BlogEntity>().ToTable("Blog", schema: "blog");

        base.OnModelCreating(modelBuilder);
    }
}