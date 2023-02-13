using MegafonATS.Models.Webhooks.Binding;
using MegafonATS.Webhooks.Models.Requests;
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
        public async Task<IActionResult> CommandAsync([FromForm, ModelBinder(BinderType = typeof(WebHookModelBinder))] WebHookModel model)
        {
            logger.LogInformation("New Request: {cmd}", Request.Form["cmd"].ToString());
            logger.LogInformation("Form body:");

            //foreach (var item in Request.Form)
            //    logger.LogInformation($"{item.Key} : {item.Value}");

            if (!TryValidateModel(model))
                return BadRequest(ModelState);

            if (model.GetType() == typeof(HistoryModel))
                await megafonAtsEvents.HistoryAsync(Request.Form["crm_token"].ToString(), model as HistoryModel, HttpContext.RequestAborted);
            else if (model.GetType() == typeof(ContactModel))
                return Ok(await megafonAtsEvents.ContactAsync(Request.Form["crm_token"].ToString(), model as ContactModel, HttpContext.RequestAborted));
            else if (model.GetType() == typeof(EventModel))
                await megafonAtsEvents.EventAsync(Request.Form["crm_token"].ToString(), model as EventModel, HttpContext.RequestAborted);
            else if (model.GetType() == typeof(RatingModel))
                await megafonAtsEvents.RatingAsync(Request.Form["crm_token"].ToString(), model as RatingModel, HttpContext.RequestAborted);
            else
            {
                logger.LogCritical("Невозможный тип модели.");
                return StatusCode(500);
            }

            return Ok();
        }
    }
}

