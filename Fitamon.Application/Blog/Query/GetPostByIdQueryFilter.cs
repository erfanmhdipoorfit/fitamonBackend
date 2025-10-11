using Fitamon.Domain.Blog.Entities;
using MediatR;

namespace Fitamon.Application.Blog.Query
{
    public class GetPostByIdQueryFilter:IRequest<BlogEntity>
    {
        public GetPostByIdQueryFilter(int blogId)
        {
            BlogId = blogId;
        }

        public int BlogId { get; set; }
    }
}
