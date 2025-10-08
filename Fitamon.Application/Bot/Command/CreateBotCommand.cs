using Fitamon.Domain.Bot.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.Command
{
   public class CreateBotCommand:IRequest<CommandResult>
    {
        public CreateBotCommand( BotEntity bot)
        {
            Bot = bot;
        }
        public BotEntity Bot { get; set; }
    }
}
