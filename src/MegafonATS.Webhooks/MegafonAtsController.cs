using MegafonATS.Models.Webhooks.Binding;
using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Models.Webhooks.Requests;
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
        public async Task<IActionResult> CommandAsync([FromForm][ModelBinder(BinderType = typeof(WebHookModelBinder))] WebHookModel model)
        {
            logger.LogInformation("New Request: {cmd}", Request.Form["cmd"].ToString());
            logger.LogInformation("Form body:");
            foreach (var item in Request.Form)
                logger.LogInformation($"{item.Key} : {item.Value}");

            if (!await megafonAtsEvents.IsValidTokenAsync(Request.Form["crm_token"].ToString(), HttpContext.RequestAborted))
                return Unauthorized("Invalid token");

            //if (!ModelMapper.MapAndValidate(Request.Form, out var webHookModel))
            //    return BadRequest();

            if (model.GetType() == typeof(HistoryModel))
                await megafonAtsEvents.HistoryAsync(model as HistoryModel);
            else if (model.GetType() == typeof(ContactModel))
                await megafonAtsEvents.ContactAsync(model as ContactModel);
            else if (model.GetType() == typeof(EventModel))
                await megafonAtsEvents.EventAsync(model as EventModel);
            else if (model.GetType() == typeof(RatingModel))
                await megafonAtsEvents.RatingAsync(model as RatingModel);
            else
            {
                logger.LogCritical("Невозможный тип модели.");
                return StatusCode(500);
            }

            return Ok();
        }
    }
}

