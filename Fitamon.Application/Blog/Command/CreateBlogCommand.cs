using Fitamon.Domain.Blog.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Blog.Command
{
    public class CreateBlogCommand:IRequest<CommandResult>
    {
        public CreateBlogCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
