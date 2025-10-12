using Fitamon.Application.Blog.Query;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Blog.Entities;
using Fitamon.Domain.Bot.Contracts;
using MediatR;

namespace Fitamon.Application.Blog.QueryHandler
{
    public class AllPostQueryHandler : IRequestHandler<AllPostQueryFilter, List<PostEntity>>
    {
        private readonly IPostServices _blogService;

        public AllPostQueryHandler(IPostServices blogService)
        {
            _blogService = blogService;
        }

        public Task<List<PostEntity>> Handle(AllPostQueryFilter request, CancellationToken cancellationToken)
        {
            return _blogService.GetAllBlog(request.PageIndex, request.PageSize);
        }
    }
}
