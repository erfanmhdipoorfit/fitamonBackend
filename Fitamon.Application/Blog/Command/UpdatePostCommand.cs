using Fitamon.Domain.Blog.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.Command
{
    public class UpdateBlogCommand:IRequest<CommandResult>
    {
        public UpdateBlogCommand(int blogId, string name)
        {
            BlogId = blogId;
            Name = name;
        }

        public int BlogId { get; }
        public string Name { get; }
    }
}
