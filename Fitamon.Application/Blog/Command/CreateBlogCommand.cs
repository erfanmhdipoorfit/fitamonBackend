using Fitamon.Domain.Blog.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.Command
{
    public class CreateBlogCommand:IRequest<CommandResult>
    {
        public CreateBlogCommand(BlogEntity blog)
        {
            Blog = blog;
        }

        public BlogEntity Blog { get; set; }
    }
}
