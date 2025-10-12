using Fitamon.Domain.Blog.Entities;
using MediatR;

namespace Fitamon.Application.Blog.Query
{
    public class GetPostByIdQueryFilter:IRequest<PostEntity>
    {
        public GetPostByIdQueryFilter(int blogId)
        {
            BlogId = blogId;
        }

        public int BlogId { get; set; }
    }
}
