using Fitamon.Application.Blog.Command;
using Fitamon.Application.Blog.Query;
using Fitamon.Domain.Blog.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seyat.Shared.Domain.Dtos;


namespace Fitamon.Endpoint.Api.Controllers.Blog
{


    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BlogController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// لیست بلاگ ها
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

        /// <summary>
        /// دریافت بلاگ با ای دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBlogById")]
        public async Task<IActionResult> GetBlogById(
             int botId)
        {

            var query = new GetBlogByIdQueryFilter(
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
        public async Task<IActionResult> Post(BlogEntity Blog)
        {
            var result = await _mediator.Send(new CreateBlogCommand(Blog));
            return Ok(result);
        }
        /// <summary>
        /// ویرایش بلاگ با ای دی
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        // PUT api/<BlogController>/5
        [HttpPut("updateBlog")]
        public async Task<IActionResult> Put(int blogId, BlogEntity blog)
        {
            if (blog == null)
                return BadRequest("Blog data is required.");

            // ✅ امنیت: مطمئن شوید که bot.Id با botId یکی است (اگر bot.Id پر شده)
            if (blog.Id != 0 && blog.Id != blogId)
                return BadRequest("Blog ID in URL does not match ID in body.");

            // همیشه از botId (از URL) استفاده کنید — این قرارداد رایج‌تر و امن‌تر است
            var result = await _mediator.Send(new UpdateBlogCommand(blogId, blog));
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

            var command = new DeleteBlogCommand(BlogIds);
            var result = await _mediator.Send(command);

            if (result.Succeed)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
