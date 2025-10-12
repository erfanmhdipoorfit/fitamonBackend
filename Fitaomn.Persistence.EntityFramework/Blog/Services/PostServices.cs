using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Blog.Entities;
using Fitamon.Domain.Bot.Entities;
using Microsoft.EntityFrameworkCore;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Persistence.EntityFramework.Blog.Services
{
    public class PostServices : IPostServices
    {
        private readonly BlogDbContext _context;

        public PostServices(BlogDbContext context) 
        {
            _context = context;
        }
        public async Task<CommandResult> CreateBlog(string name)
        {
            if (name == null)
                return new CommandResult(false, "blog data cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                return new CommandResult(false, "blog name is required.");

            try
            {
           
                var blog = new PostEntity
                {
                    Name = name.Trim()
                };
                _context.Posts.Add(blog);
                await _context.SaveChangesAsync();

        
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
                var botsToDelete = await _context.Posts
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
                _context.Posts.RemoveRange(botsToDelete);
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
        public async Task<List<PostEntity>> GetAllBlog(int pageIndex, int pageSize)
        {
            // اعتبارسنجی ورودی
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            // خواندن از دیتابیس + پیجینیشن
            var pagedBots = await _context.Posts
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return pagedBots;
        }
        public async Task<PostEntity> GetBlogById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("blog ID must be greater than zero.", nameof(id));

            var blog = await _context.Posts.FindAsync(id);
            if (blog == null)
                throw new KeyNotFoundException($"blog with ID {id} not found.");

            return blog;
        }
        public async Task<CommandResult> UpdateBlogById(int id, string name)
        {
            // اعتبارسنجی ورودی
            if (string.IsNullOrWhiteSpace(name))
                return new CommandResult(false, "Blog name is required.");

            name = name.Trim();

            // (اختیاری) محدودیت طول نام
            if (name.Length > 100)
                return new CommandResult(false, "Blog name cannot exceed 100 characters.");

            try
            {
                // پیدا کردن بلاگ موجود
                var existingBlog = await _context.Posts.FindAsync(id);
                if (existingBlog == null)
                    return new CommandResult(false, $"Blog with ID {id} not found.");

                // (اختیاری) جلوگیری از تکرار نام — فقط اگر نام تغییر کرده باشد
                if (existingBlog.Name != name &&
                    await _context.Posts.AnyAsync(b => b.Name == name))
                {
                    return new CommandResult(false, "A blog with this name already exists.");
                }

                // آپدیت نام
                existingBlog.Name = name;

                // ذخیره تغییرات
                await _context.SaveChangesAsync();

                return new CommandResult(true, "Blog updated successfully.", existingBlog);
            }
            catch (Exception ex)
            {
                // نمایش دقیق‌تر خطا برای دیباگ
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return new CommandResult(false, $"Failed to update blog: {errorMessage}");
            }
        }

    }
}
