using Fitamon.Application.Blog.Command;
using Fitamon.Application.Blog.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seyat.Shared.Domain.Dtos;


namespace Fitamon.Endpoint.Api.Controllers.Blog
{


    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PostController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// لیست بلاگ ها
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> Lists(
             int pageIndex, int pageSize)
        {

            var query = new AllPostQueryFilter(
                pageIndex,
                pageSize

              );
            var result = await _mediator.Send(query);
            return Ok(result);


        }

        /// <summary>
        /// دریافت بلاگ با ای دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBlogById")]
        public async Task<IActionResult> GetBlogById(
             int botId)
        {

            var query = new GetPostByIdQueryFilter(
                botId
              );
            var result = await _mediator.Send(query);
            return Ok(result);


        }

        /// <summary>
        /// ساخت بلاگ
        /// </summary>
        /// <param name="bot"></param>
        /// <returns></returns>
        [HttpPost("createBlog")]
        public async Task<IActionResult> Post(CreatePostCommand filter, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(filter, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// ویرایش بلاگ با ای دی
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        [HttpPut("updateBlog")]
        public async Task<IActionResult> Put(int blogId, string name)
        {
            if (name == null)
                return BadRequest("Blog data is required.");
            var result = await _mediator.Send(new UpdateBlogCommand(blogId, name));
            return Ok(result);
        }

        /// <summary>
        /// حذف بلاگ با ای دی
        /// </summary>
        /// <param name="BotIds"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        public async Task<IActionResult> Delete([FromQuery] List<int> BlogIds)
        {

            if (BlogIds == null || !BlogIds.Any())
            {
                return BadRequest(new CommandResult(false, "blog IDs list cannot be null or empty.", 400, null));
            }

            var command = new DeletePostCommand(BlogIds);
            var result = await _mediator.Send(command);

            if (result.Succeed)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
