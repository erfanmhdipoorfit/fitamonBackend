using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitamon.Persistence.EntityFramework.Blog.Services
{
    public class BlogServices : IBlogServices
    {
        private readonly BlogDbContext _context;

        public BlogServices(BlogDbContext context) // ✅ DI می‌کنه
        {
            _context = context;
        }

        public async Task<List<BlogEntity>> GetAllBlog(int pageIndex, int pageSize)
        {
            // اعتبارسنجی ورودی
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            // خواندن از دیتابیس + پیجینیشن
            var pagedBots = await _context.Blogs
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return pagedBots;
        }
    }
}
