using Microsoft.EntityFrameworkCore;
using BotEntity = Fitamon.Domain.Bot.Entities.Bot;
namespace Fitamon.Persistence.EntityFramework.Bot
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
     : base(options)
        {
        }
        public DbSet<BotEntity> Bots { get; set; }
    }
}
