using MegafonATS.Models.Webhooks.Binding;
using MegafonATS.Webhooks.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace MegafonATS.Webhooks
{
    [Route("megafon")]
    [ApiController]
    public class MegafonAtsController : ControllerBase
    {
        readonly IMegafonAtsEvents megafonAtsEvents;
        readonly ILogger<MegafonAtsController> logger;

        public MegafonAtsController(IMegafonAtsEvents megafonAtsEvents, ILogger<MegafonAtsController> logger)
        {
            this.megafonAtsEvents = megafonAtsEvents;
            this.logger = logger;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> CommandAsync([FromForm(Name = "crm_token"), Required] string token, [FromForm, Required, ModelBinder(BinderType = typeof(WebHookModelBinder))] WebHookModel model)
        {
            logger.LogInformation("New Request: {cmd}", Request.Form["cmd"].ToString());

            if (!TryValidateModel(model))
                return BadRequest(ModelState);

            if (model.GetType() == typeof(HistoryModel))
                await megafonAtsEvents.HistoryAsync(token, model as HistoryModel, HttpContext.RequestAborted);
            else if (model.GetType() == typeof(ContactModel))
                return Ok(await megafonAtsEvents.ContactAsync(token, model as ContactModel, HttpContext.RequestAborted));
            else if (model.GetType() == typeof(EventModel))
                await megafonAtsEvents.EventAsync(token, model as EventModel, HttpContext.RequestAborted);
            else if (model.GetType() == typeof(RatingModel))
                await megafonAtsEvents.RatingAsync(token, model as RatingModel, HttpContext.RequestAborted);
            else
            {
                logger.LogCritical("Невозможный тип модели.");
                return StatusCode(500);
            }

            return Ok();
        }
    }
}

