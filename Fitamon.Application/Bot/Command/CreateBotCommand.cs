using Fitamon.Domain.Bot.Entities;
using MediatR;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Application.Bot.Command
{
   public class CreateBotCommand:IRequest<CommandResult>
    {
        public CreateBotCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
