using Fitamon.Application.Blog.Query;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Blog.Entities;
using Fitamon.Domain.Bot.Contracts;
using MediatR;

namespace Fitamon.Application.Blog.QueryHandler
{
    public class AllBlogQueryHandler : IRequestHandler<AllBlogQueryFilter, List<BlogEntity>>
    {
        private readonly IBlogServices _blogService;

        public AllBlogQueryHandler(IBlogServices blogService)
        {
            _blogService = blogService;
        }

        public Task<List<BlogEntity>> Handle(AllBlogQueryFilter request, CancellationToken cancellationToken)
        {
            return _blogService.GetAllBlog(request.PageIndex, request.PageSize);
        }
    }
}
