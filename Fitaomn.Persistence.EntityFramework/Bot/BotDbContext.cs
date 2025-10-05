using Fitaomn.Persistence.EntityFramework.Blog;
using Microsoft.EntityFrameworkCore;
using BotModel = Fitamon.Domain.Bot.Entities.Bot;
namespace Fitaomn.Persistence.EntityFramework.Bot
{
    public class BotDbContext:DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
     : base(options)
        {
        }
        public DbSet<BotModel> Blog { get; set; }
    }
}
