using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Webhooks.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MegafonATS.Webhooks
{
    [Route("megafon")]
    [ApiController]
    public class MegafonAtsController : Controller
    {
        readonly IMegafonAtsEvents megafonAtsEvents;
        readonly ILogger<MegafonAtsController> logger;
        public MegafonAtsController(IMegafonAtsEvents megafonAtsEvents, ILogger<MegafonAtsController> logger)
        {
            this.megafonAtsEvents = megafonAtsEvents;
            this.logger = logger;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> CommandAsync()
        {
            logger.LogInformation("New Request: {cmd}", Request.Form["cmd"].ToString());
            logger.LogInformation("Form body:");
            foreach (var item in Request.Form)
                logger.LogInformation($"{item.Key} : {item.Value}");

            if (!await megafonAtsEvents.IsValidTokenAsync(Request.Form["crm_token"].ToString(), HttpContext.RequestAborted))
                return Unauthorized("Invalid token");

            if (!ModelMapper.MapAndValidate(Request.Form, out var webHookModel))
                return BadRequest();

            if (webHookModel.GetType() == typeof(HistoryModel))
                await megafonAtsEvents.HistoryAsync(webHookModel as HistoryModel);
            else if (webHookModel.GetType() == typeof(ContactModel))
                await megafonAtsEvents.ContactAsync(webHookModel as ContactModel);
            else if (webHookModel.GetType() == typeof(EventModel))
                await megafonAtsEvents.EventAsync(webHookModel as EventModel);
            else if (webHookModel.GetType() == typeof(RatingModel))
                await megafonAtsEvents.RatingAsync(webHookModel as RatingModel);
            else
            {
                logger.LogCritical("Невозможный тип модели.");
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
