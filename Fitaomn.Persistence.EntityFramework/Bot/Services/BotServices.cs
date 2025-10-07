
using Fitamon.Domain.Bot.Contracts;
using Fitamon.Domain.Bot.Entities;
using Microsoft.EntityFrameworkCore;
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
}
