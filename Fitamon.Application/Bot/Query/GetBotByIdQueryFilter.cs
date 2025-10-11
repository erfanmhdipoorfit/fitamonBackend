using Fitamon.Domain.Bot.Entities;
using MediatR;

namespace Fitamon.Application.Bot.Query
{
   public class GetBlogByIdQueryFilter:IRequest<BotEntity>
    {
        public GetBlogByIdQueryFilter(
      int botId)
        {
            BotId = botId;
        }

        public int BotId { get; private set; }
    }
}
