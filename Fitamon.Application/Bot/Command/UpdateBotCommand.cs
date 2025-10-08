using Fitamon.Application.Bot.Query;
using Fitamon.Domain.Bot.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.Command
{
   public class UpdateBotCommand:IRequest<CommandResult>
    {
        public UpdateBotCommand(int botId,BotEntity bot)
        {
            BotId = botId;
            Bot = bot;
        }

        public int BotId { get; set; }
        public BotEntity Bot { get; set; }
    }
}
