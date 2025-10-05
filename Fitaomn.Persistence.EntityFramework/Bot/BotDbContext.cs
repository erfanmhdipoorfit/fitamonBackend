using Microsoft.EntityFrameworkCore;
using BotModel = Fitamon.Domain.Bot.Entities.Bot;
namespace Fitaomn.Persistence.EntityFramework.Bot
{
    public class BotDbContext
    {
        public DbSet<BotModel> Blog { get; set; }
    }
}
