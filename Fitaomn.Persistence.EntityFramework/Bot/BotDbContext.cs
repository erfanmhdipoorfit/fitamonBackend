using Fitamon.Domain.Bot.Entities;
using Microsoft.EntityFrameworkCore;

public class BotDbContext : DbContext
{
    public BotDbContext(DbContextOptions<BotDbContext> options) : base(options) { }

    public DbSet<BotEntity> Bots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // تنظیم اسکیما به "bot"
        modelBuilder.Entity<BotEntity>().ToTable("Bot", schema: "bot");

        base.OnModelCreating(modelBuilder);
    }
}
