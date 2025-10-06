
using Fitamon.Domain.Bot.Contracts;
using BotEntity = Fitamon.Domain.Bot.Entities.Bot;
using System;

namespace Fitamon.Persistence.EntityFramework.Bot.Services
{
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
            // داده‌های نمونه (در واقعیت از دیتابیس می‌آید)
            var allBots = new List<BotEntity>
    {
        new BotEntity { Name = "FitnessBot" },
        new BotEntity { Name = "DietCoach" },
        new BotEntity { Name = "WorkoutPal" },
        new BotEntity { Name = "YogaGuide" },
        new BotEntity { Name = "MealPlanner" },
        new BotEntity { Name = "RunningCoach" },
        new BotEntity { Name = "SleepTracker" }
    };

            // ✅ اعمال پیجینیشن
            var pagedBots = allBots
                .Skip((pageIndex - 1) * pageSize) // صفحه‌ها معمولاً از 1 شروع می‌شوند
                .Take(pageSize)
                .ToList();

            return await Task.FromResult(pagedBots);
        }
    }
}
