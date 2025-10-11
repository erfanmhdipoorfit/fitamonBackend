using Fitamon.Application.Bot.Query;
using Fitamon.Domain.Bot.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.Command
{
   public class UpdateBotCommand:IRequest<CommandResult>
    {
        public UpdateBotCommand(int botId,string name)
        {
            BotId = botId;
            Name = name;
        }

        public int BotId { get; set; }
        public string Name { get; set; }
    }
}
