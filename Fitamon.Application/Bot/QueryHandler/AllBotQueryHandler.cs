using Fitamon.Application.Bot.Query;
using Fitamon.Domain.Bot.Contracts;
using Fitamon.Domain.Bot.Entities;

//using BotEntity = Fitamon.Domain.Bot.Entities.BotEntity;
using MediatR;

namespace Fitamon.Application.Bot.QueryHandler
{
    public class AllBotQueryHandler : IRequestHandler
        <AllBotQueryFilter, List<BotEntity>>

    {

        private readonly IBotServices _botService;

        public AllBotQueryHandler(IBotServices botService)
        {
            _botService = botService;
        }
        public Task<List<BotEntity>> Handle(AllBotQueryFilter request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return _botService.GetAllBot(request.PageIndex,request.PageSize);
        }
    }
}
