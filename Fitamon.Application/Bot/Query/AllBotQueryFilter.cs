using MediatR;
using BotEntity = Fitamon.Domain.Bot.Entities.Bot;
namespace Fitamon.Application.Bot.Query
{
    public class AllBotQueryFilter : IRequest<List<BotEntity>>
    {
        public AllBotQueryFilter(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public AllBotQueryFilter(int pageIndex, int pageSize, object value)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Value = value;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public object Value { get; }
    }
}
