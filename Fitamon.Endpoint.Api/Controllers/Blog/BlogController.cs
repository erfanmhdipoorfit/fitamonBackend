using Fitamon.Application.Blog.Query;
using Fitamon.Application.Bot.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fitamon.Endpoint.Api.Controllers.Blog
{


    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BlogController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// لیست ربات ها
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetAllBlogs")]

        public async Task<IActionResult> Lists(
             int pageIndex, int pageSize)
        {

            var query = new AllBlogQueryFilter(
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
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
