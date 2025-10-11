using Fitamon.Application.Bot.Query;
using Fitamon.Domain.Bot.Contracts;
using Fitamon.Domain.Bot.Entities;
using MediatR;

namespace Fitamon.Application.Bot.QueryHandler
{
    public class GetBotByIdQueryHandler : IRequestHandler<GetBlogByIdQueryFilter, BotEntity>
    {

        private readonly IBotServices _botService;

        public GetBotByIdQueryHandler(IBotServices botService)
        {
            _botService = botService;
        }
        public Task<BotEntity> Handle(GetBlogByIdQueryFilter request, CancellationToken cancellationToken)
        {
            return _botService.GetBotById(request.BotId);
        }
    }
}
