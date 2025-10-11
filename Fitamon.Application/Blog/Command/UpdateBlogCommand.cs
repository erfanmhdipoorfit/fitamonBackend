using Fitamon.Domain.Blog.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.Command
{
    public class UpdateBlogCommand:IRequest<CommandResult>
    {
        public UpdateBlogCommand(int blogId, BlogEntity blog)
        {
            BlogId = blogId;
            Blog = blog;
        }

        public int BlogId { get; }
        public BlogEntity Blog { get; }
    }
}
