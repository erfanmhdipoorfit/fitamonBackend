using Fitamon.Application.Bot.Command;
using Fitamon.Domain.Bot.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.CommandHandler
{
    public class DeleteBotCommandHandler : IRequestHandler<DeleteBotCommand, CommandResult>
    {
        private readonly IBotServices _botService;

        public DeleteBotCommandHandler(IBotServices botService)
        {
            _botService = botService;
        }
        public Task<CommandResult> Handle(DeleteBotCommand request, CancellationToken cancellationToken)
        {
            return _botService.DeleteBotById(request.BotIds);
        }
    }
}
