using Fitamon.Application.Bot.Command;
using Fitamon.Application.Bot.Query;
using Fitamon.Domain.Bot.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seyat.Shared.Domain.Dtos;

namespace Fitamon.Endpoint.Api.Controllers.Bot
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : Controller
    {
        private readonly IMediator _mediator;

        public BotController(IMediator mediator) => _mediator = mediator;


        /// <summary>
        /// لیست ربات ها
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetAllBots")]
 
        public async Task<IActionResult> Lists(
             int pageIndex, int pageSize)
        {

            var query = new AllBotQueryFilter(
                pageIndex,
                pageSize

              );
            var result = await _mediator.Send(query);
            return Ok(result);


        }

        /// <summary>
        /// دریافت ربات با ای دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBotById")]
        public async Task<IActionResult> GetBotById(
             int botId)
        {

            var query = new GetBotByIdQueryFilter(
                botId

              );
            var result = await _mediator.Send(query);
            return Ok(result);


        }

        /// <summary>
        /// ساخت ربات
        /// </summary>
        /// <param name="bot"></param>
        /// <returns></returns>
        [HttpPost("createBot")]
        public async Task<IActionResult> Post(BotEntity bot)
        {
            var result = await _mediator.Send(new CreateBotCommand( bot));
            return Ok(result);
        }

        /// <summary>
        /// ویرایش ربات با ای دی
        /// </summary>
        /// <param name="bot"></param>
        /// <returns></returns>
        [HttpPut("updateBot")]
        public async Task<IActionResult> Put(int botId, BotEntity bot)
        {
            if (bot == null)
                return BadRequest("Bot data is required.");

            // ✅ امنیت: مطمئن شوید که bot.Id با botId یکی است (اگر bot.Id پر شده)
            if (bot.Id != 0 && bot.Id != botId)
                return BadRequest("Bot ID in URL does not match ID in body.");

            // همیشه از botId (از URL) استفاده کنید — این قرارداد رایج‌تر و امن‌تر است
            var result = await _mediator.Send(new UpdateBotCommand(botId, bot));
            return Ok(result);

        }

        /// <summary>
        /// حذف ربات با ای دی
        /// </summary>
        /// <param name="BotIds"></param>
        /// <returns></returns>
        [HttpDelete("deleteBot")]
        public async Task<IActionResult> Delete([FromQuery] List<int> BotIds)
        {

            if (BotIds == null || !BotIds.Any())
            {
                return BadRequest(new CommandResult(false, "Bot IDs list cannot be null or empty.", 400, null));
            }

            var command = new DeleteBotCommand(BotIds);
            var result = await _mediator.Send(command);

            if (result.Succeed)
                return Ok(result);
            else
                return BadRequest(result);

        }
    }
}
