using Fitamon.Application.Bot.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitamon.Endpoint.Api.Controllers.Bot
{
    public class BotController : Controller
    {
        private readonly IMediator _mediator;

        public BotController(IMediator mediator) => _mediator = mediator;


        /// <summary>
        /// لیست
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        //[ActionFaName("لیست")]
        public async Task<IActionResult> List(
             int pageIndex, int pageSize)
        {

            var query = new AllBotQueryFilter(
                pageIndex,
                pageSize

              );
            var result = await _mediator.Send(query);
            return Ok(result);


        }
    }
}
