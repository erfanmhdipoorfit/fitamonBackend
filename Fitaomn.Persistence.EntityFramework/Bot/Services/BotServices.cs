
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
            return await Task.FromResult(new List<BotEntity>
    {
        new BotEntity { Name = "FitnessBot" },
        new BotEntity { Name = "DietCoach" },
        new BotEntity { Name = "WorkoutPal" }
    });
        }
    }
}
