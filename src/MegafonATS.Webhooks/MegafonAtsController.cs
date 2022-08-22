using MegafonATS.Models.Webhooks;
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
            var cmd = Request.Form["cmd"];
            var token = Request.Form["crm_token"];

            if (!await megafonAtsEvents.IsValidTokenAsync(token, HttpContext.RequestAborted)) return Unauthorized("Invalid token");

            switch (cmd)
            {
                case "history":
                    {

                        HistoryModel model = new();
                        if (!await TryUpdateModelAsync(model))
                        {
                            if (DateTime.TryParseExact(Request.Form["start"],
                                                        "yyyyMMddThhmmssT",
                                                        System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out DateTime tmp))
                            {
                                model.Start = tmp;
                            }
                            else return BadRequest("Invalid parameters");
                        }

                        try
                        {
                            await megafonAtsEvents.HistoryAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message, e);
                            return BadRequest("Invalid parameters");
                        }

                        return Ok("history");
                    }
                case "event":
                    {
                        EventModel model = new();
                        if (!await TryUpdateModelAsync(model)) return BadRequest("Invalid parameters");
                        try
                        {
                            await megafonAtsEvents.EventAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message, e);
                            return BadRequest("Invalid parameters");
                        }
                        return Ok("event");
                    }
                case "contact":
                    {
                        ContactModel model = new();
                        if (!await TryUpdateModelAsync(model)) return BadRequest("Invalid parameters"); _ = await TryUpdateModelAsync(model);
                        try
                        {
                            await megafonAtsEvents.ContactAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message, e);
                            return BadRequest("Invalid parameters");
                        }
                        return Ok("contact");
                    }
                case "rating":
                    {
                        RatingModel model = new();
                        if (!await TryUpdateModelAsync(model)) return BadRequest("Invalid parameters");
                        try
                        {
                            await megafonAtsEvents.RatingAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message, e);
                            return BadRequest("Invalid parameters");
                        }
                        return Ok("rating");
                    }
                default:
                    return BadRequest();
            }
        }
    }
}