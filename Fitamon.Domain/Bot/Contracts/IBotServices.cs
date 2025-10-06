using BotEntity = Fitamon.Domain.Bot.Entities.Bot;

namespace Fitamon.Domain.Bot.Contracts
{
    public interface IBotServices
    {
        Task<List<BotEntity>> GetAllBot(int pageIndex,int pageSize);
    }
}
