using Fitamon.Application.Bot.Command;
using Fitamon.Domain.Bot.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.CommandHandler
{
    public class UpdateBotCommandHandler : IRequestHandler<UpdateBotCommand, CommandResult>
    {

        private readonly IBotServices _botService;

        public UpdateBotCommandHandler(IBotServices botService)
        {
            _botService = botService;
        }
        public Task<CommandResult> Handle(UpdateBotCommand request, CancellationToken cancellationToken)
        {
            return _botService.UpdateBotById(request.BotId,request.Name);
        }
    }
}
