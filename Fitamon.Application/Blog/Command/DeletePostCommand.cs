

using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.Command
{
    public class DeletePostCommand:IRequest<CommandResult>
    {
        public DeletePostCommand(List<int> blogIds)
        {
            BlogIds = blogIds;
        }

        public List<int> BlogIds { get; }
    }
}
