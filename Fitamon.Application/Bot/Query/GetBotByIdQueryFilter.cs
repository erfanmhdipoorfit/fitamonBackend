using Fitamon.Domain.Bot.Entities;
using MediatR;

namespace Fitamon.Application.Bot.Query
{
   public class GetBotByIdQueryFilter:IRequest<BotEntity>
    {
        public GetBotByIdQueryFilter(
      int botId)
        {
            BotId = botId;
        }

        public int BotId { get; private set; }
    }
}
