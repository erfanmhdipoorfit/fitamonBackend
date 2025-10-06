using Microsoft.EntityFrameworkCore;
using BlogEntity = Fitamon.Domain.Blog.Entities.Blog;

namespace Fitamon.Persistence.EntityFramework.Blog
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
      : base(options)
        {
        }
        public DbSet<BlogEntity> Blog { get; set; }
    }
}
