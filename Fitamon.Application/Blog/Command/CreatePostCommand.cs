using Fitamon.Domain.Blog.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.Command
{
    public class CreatePostCommand:IRequest<CommandResult>
    {
        public CreatePostCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
