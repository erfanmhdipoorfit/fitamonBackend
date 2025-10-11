

using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.Command
{
    public class DeleteBlogCommand:IRequest<CommandResult>
    {
        public DeleteBlogCommand(List<int> blogIds)
        {
            BlogIds = blogIds;
        }

        public List<int> BlogIds { get; }
    }
}
