//using BotEntity = Fitamon.Domain.Bot.Entities.BotEntity;

using Fitamon.Domain.Bot.Entities;

namespace Fitamon.Domain.Bot.Contracts
{
    public interface IBotServices
    {
        Task<List<BotEntity>> GetAllBot(int pageIndex,int pageSize);
    }
}
