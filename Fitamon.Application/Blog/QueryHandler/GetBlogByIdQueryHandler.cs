using Fitamon.Application.Blog.Query;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Blog.Entities;
using MediatR;

namespace Fitamon.Application.Blog.QueryHandler
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQueryFilter, BlogEntity>
    {
        private readonly IBlogServices _blogService;

        public GetBlogByIdQueryHandler(IBlogServices blogService)
        {
            _blogService = blogService;
        }

        public Task<BlogEntity> Handle(GetBlogByIdQueryFilter request, CancellationToken cancellationToken)
        {
            return _blogService.GetBlogById(request.BlogId);
        }
    }
}
