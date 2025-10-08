using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.Command
{
    public class DeleteBotCommand : IRequest<CommandResult>
    {
        public DeleteBotCommand(List<int> botIds)
        {
            BotIds = botIds;
        }

        public List<int> BotIds { get; set; }
    }
}
