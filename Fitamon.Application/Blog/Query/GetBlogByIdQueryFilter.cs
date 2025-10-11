using Fitamon.Domain.Blog.Entities;
using MediatR;

namespace Fitamon.Application.Blog.Query
{
    public class GetBlogByIdQueryFilter:IRequest<BlogEntity>
    {
        public GetBlogByIdQueryFilter(int blogId)
        {
            BlogId = blogId;
        }

        public int BlogId { get; set; }
    }
}
