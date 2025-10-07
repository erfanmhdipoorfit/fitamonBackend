namespace Fitamon.Domain.Bot.Entities
{
    public class BotEntity
    {
        public int Id { get; set; }          // ✅ EF Core به‌صورت خودکار این رو Primary Key می‌دونه
        public string Name { get; set; } = string.Empty;
    }
}
