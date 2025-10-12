using Fitamon.Application.Blog.Query;
using Fitamon.Domain.Blog.Contracts;
using Fitamon.Domain.Blog.Entities;
using MediatR;

namespace Fitamon.Application.Blog.QueryHandler
{
    public class GetPsotByIdQueryHandler : IRequestHandler<GetPostByIdQueryFilter, PostEntity>
    {
        private readonly IPostServices _blogService;

        public GetPsotByIdQueryHandler(IPostServices blogService)
        {
            _blogService = blogService;
        }

        public Task<PostEntity> Handle(GetPostByIdQueryFilter request, CancellationToken cancellationToken)
        {
            return _blogService.GetBlogById(request.BlogId);
        }
    }
}
