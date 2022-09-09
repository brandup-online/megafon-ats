using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Models.Webhooks.ResponseModels;
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

            logger.LogInformation("New Request: {cmd}", cmd);
            logger.LogInformation("Form body:");
            foreach (var item in Request.Form)
                logger.LogInformation($"{item.Key} : {item.Value}");

            if (!await megafonAtsEvents.IsValidTokenAsync(token, HttpContext.RequestAborted)) return Unauthorized("Invalid token");

            switch (cmd)
            {
                case "history":
                    {

                        HistoryModel model = new();
                        if (!await TryUpdateModelAsync(model))
                        {
                            if (DateTime.TryParseExact(Request.Form["start"],
                                                        "yyyy-MM-ddThh:mm:ssZ",
                                                        System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out DateTime tmp))
                            {
                                model.Start = tmp;
                            }
                            else
                            {
                                logger.LogError($"Неудалось привязать модель {nameof(HistoryModel)}");
                                return BadRequest("Invalid parameters");
                            }
                        }

                        try
                        {
                            await megafonAtsEvents.HistoryAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message);
                            return BadRequest("Invalid parameters");
                        }

                        return Ok("history");
                    }
                case "event":
                    {
                        EventModel model = new();
                        if (!await TryUpdateModelAsync(model))
                        {
                            logger.LogError($"Неудалось привязать модель {nameof(EventModel)}");
                            return BadRequest("Invalid parameters");
                        }

                        try
                        {
                            await megafonAtsEvents.EventAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message);
                            return BadRequest("Invalid parameters");
                        }
                        return Ok("event");
                    }
                case "contact":
                    {
                        ContactModel model = new();
                        ContactResponse response = new();
                        if (!await TryUpdateModelAsync(model))
                        {
                            logger.LogError($"Неудалось привязать модель {nameof(ContactModel)}");
                            return BadRequest("Invalid parameters");
                        }

                        try
                        {
                            response = await megafonAtsEvents.ContactAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message);
                            return BadRequest("Invalid parameters");
                        }

                        return Ok(response);
                    }
                case "rating":
                    {
                        RatingModel model = new();
                        if (!await TryUpdateModelAsync(model))
                        {
                            logger.LogError($"Неудалось привязать модель {nameof(RatingModel)}");
                            return BadRequest("Invalid parameters");
                        }

                        try
                        {
                            await megafonAtsEvents.RatingAsync(model, HttpContext.RequestAborted);
                        }
                        catch (Exception e)
                        {
                            logger.LogCritical(e.Message);
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