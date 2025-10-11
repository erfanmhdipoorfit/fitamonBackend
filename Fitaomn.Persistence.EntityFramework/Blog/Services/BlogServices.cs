using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Persistence.EntityFramework.Blog.Services
{
    public class BlogServices : IBlogServices
    {
        private readonly BlogDbContext _context;

        public BlogServices(BlogDbContext context) 
        {
            _context = context;
        }
        public async Task<CommandResult> CreateBlog(BlogEntity blog)
        {
            if (blog == null)
                return new CommandResult(false, "blog data cannot be null.");

            if (string.IsNullOrWhiteSpace(blog.Name))
                return new CommandResult(false, "blog name is required.");

            try
            {
                // Id به‌صورت خودکار توسط دیتابیس تنظیم می‌شود (auto-increment)
                // پس نیازی به ست کردن آن نیست
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();

                // پس از ذخیره، bot.Id پر شده است
                return new CommandResult(true, "blog created successfully.", blog);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, $"Failed to create blog: {ex.Message}");
            }
        }
        public async Task<CommandResult> DeleteBlog(List<int> botIds)
        {
            // اعتبارسنجی ورودی
            if (botIds == null || !botIds.Any())
            {
                return new CommandResult(
                    succeed: false,
                    message: "Bot IDs list cannot be null or empty.",
                    errorCode: 400,// Bad Request
                      data: (dynamic?)null
                );
            }

            // فیلتر کردن IDهای معتبر (مثبت)
            var validIds = botIds.Where(id => id > 0).ToList();
            if (!validIds.Any())
            {
                return new CommandResult(
                    succeed: false,
                    message: "No valid bot IDs provided.",
                    errorCode: 400,
                      data: (dynamic?)null
                );
            }

            try
            {
                // گرفتن رکوردهای موجود برای حذف
                var botsToDelete = await _context.Blogs
                    .Where(b => validIds.Contains(b.Id))
                    .ToListAsync();

                if (!botsToDelete.Any())
                {
                    return new CommandResult(
                        succeed: false,
                        message: "None of the provided bot IDs were found.",
                        errorCode: 404, // Not Found
                          data: (dynamic?)null
                    );
                }

                // حذف رکوردها
                _context.Blogs.RemoveRange(botsToDelete);
                await _context.SaveChangesAsync();

                // اختیاری: می‌تونی IDهای حذف‌شده یا تعدادشون رو برگردونی
                var deletedIds = botsToDelete.Select(b => b.Id).ToList();

                return new CommandResult(
                    succeed: true,
                    message: $"{deletedIds.Count} bot(s) deleted successfully.",
                    data: new { DeletedCount = deletedIds.Count, DeletedIds = deletedIds }
                );
            }
            catch (Exception ex)
            {
                // در محیط واقعی، حتماً لاگ کن!
                return new CommandResult(
                    succeed: false,
                    message: "An unexpected error occurred while deleting bots.",
                    errorCode: 500, // Internal Server Error
                    data: null
                );
            }
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
        public async Task<BlogEntity> GetBlogById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("blog ID must be greater than zero.", nameof(id));

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                throw new KeyNotFoundException($"blog with ID {id} not found.");

            return blog;
        }
        public async Task<CommandResult> UpdateBlogById(int id, BlogEntity blog)
        {
            if (blog == null)
                return new CommandResult(false, "Bot data cannot be null.");

            var existingBot = await _context.Blogs.FindAsync(id);
            if (existingBot == null)
                return new CommandResult(false, $"Bot with ID {id} not found.");


            _context.Entry(existingBot).CurrentValues.SetValues(blog);

            try
            {
                await _context.SaveChangesAsync();
                return new CommandResult(true, "blog updated successfully.", existingBot);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, $"Failed to update blog: {ex.Message}");
            }
        }

    }
}
