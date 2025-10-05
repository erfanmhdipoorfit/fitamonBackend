using Microsoft.EntityFrameworkCore;
using BlogModel = Fitamon.Domain.Blog.Entities.Blog;

namespace Fitaomn.Persistence.EntityFramework.Blog
{
    public class BlogDbContext:DbContext
    {
        public DbSet<BlogModel> Blog { get; set; }
    }
}
