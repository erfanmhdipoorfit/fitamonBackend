using Fitamon.Application.Bot.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitamon.Endpoint.Api.Controllers.Bot
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
