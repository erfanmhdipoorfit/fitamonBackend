//using BotEntity = Fitamon.Domain.Bot.Entities.BotEntity;

using Fitamon.Domain.Bot.Entities;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Domain.Bot.Contracts
{
    public interface IBotServices
    {
        Task<List<BotEntity>> GetAllBot(int pageIndex,int pageSize);
        Task<BotEntity> GetBotById(int id);
        Task<CommandResult> DeleteBotById(List<int> botIds);
        Task<CommandResult> UpdateBotById(int botId ,BotEntity bot);
        Task<CommandResult> CreateBot(BotEntity bot);


    }
}
