using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitamon.Endpoint.Api.Controllers.Bot
{
    public class BotController : Controller
    {
        private readonly IMediator _mediator;

        public BotController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
            => Ok(await _mediator.Send(command));
    }
}
