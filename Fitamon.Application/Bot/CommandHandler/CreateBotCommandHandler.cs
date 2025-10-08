using Fitamon.Application.Blog.Command;
using Fitamon.Application.Bot.Command;
using Fitamon.Domain.Bot.Contracts;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.CommandHandler
{
    public class CreateBotCommandHandler : IRequestHandler<CreateBotCommand, CommandResult>
    {
        private readonly IBotServices _botService;

        public CreateBotCommandHandler(IBotServices botService)
        {
            _botService = botService;
        }
        public Task<CommandResult> Handle(CreateBotCommand request, CancellationToken cancellationToken)
        {
            return _botService.CreateBot( request.Bot);
        }
    }
}
