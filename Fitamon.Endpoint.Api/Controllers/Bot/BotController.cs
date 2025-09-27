using Microsoft.AspNetCore.Mvc;

namespace Fitamon.Endpoint.Api.Controllers.Bot
{
    public class BotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
