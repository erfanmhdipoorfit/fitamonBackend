using Fitamon.Domain.Bot.Contracts;
using Fitamon.Domain.Bot.Entities;
using Microsoft.EntityFrameworkCore;
using Seyat.Shared.Domain.Dtos;
//using BotEntity = Fitamon.Domain.Bot.Entities.BotEntity;

namespace Fitamon.Persistence.EntityFramework.Bot.Services;

public class BotServices : IBotServices


{
    private readonly BotDbContext _context;
    public BotServices(
        BotDbContext context
      )
    {
        _context = context;
    }

    public async Task<List<BotEntity>> GetAllBot(int pageIndex, int pageSize)
    {
        // اعتبارسنجی ورودی
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;

        // خواندن از دیتابیس + پیجینیشن
        var pagedBots = await _context.Bots
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return pagedBots;
    }

    public async Task<BotEntity> GetBotById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Bot ID must be greater than zero.", nameof(id));

        var bot = await _context.Bots.FindAsync(id);
        if (bot == null)
            throw new KeyNotFoundException($"Bot with ID {id} not found.");

        return bot;
    }

    public async Task<CommandResult> DeleteBotById(List<int> botIds)
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
            var botsToDelete = await _context.Bots
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
            _context.Bots.RemoveRange(botsToDelete);
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

    public async Task<CommandResult> UpdateBotById(int id, BotEntity bot)
    {
        if (bot == null)
            return new CommandResult(false, "Bot data cannot be null.");

        var existingBot = await _context.Bots.FindAsync(id);
        if (existingBot == null)
            return new CommandResult(false, $"Bot with ID {id} not found.");

      
        _context.Entry(existingBot).CurrentValues.SetValues(bot);

        try
        {
            await _context.SaveChangesAsync();
            return new CommandResult(true, "Bot updated successfully.", existingBot);
        }
        catch (Exception ex)
        {
            return new CommandResult(false, $"Failed to update bot: {ex.Message}");
        }
    }

    public async Task<CommandResult> CreateBot(BotEntity bot)
    {
        if (bot == null)
            return new CommandResult(false, "Bot data cannot be null.");

        if (string.IsNullOrWhiteSpace(bot.Name))
            return new CommandResult(false, "Bot name is required.");

        try
        {
            // Id به‌صورت خودکار توسط دیتابیس تنظیم می‌شود (auto-increment)
            // پس نیازی به ست کردن آن نیست
            _context.Bots.Add(bot);
            await _context.SaveChangesAsync();

            // پس از ذخیره، bot.Id پر شده است
            return new CommandResult(true, "Bot created successfully.", bot);
        }
        catch (Exception ex)
        {
            return new CommandResult(false, $"Failed to create bot: {ex.Message}");
        }
    }
}
