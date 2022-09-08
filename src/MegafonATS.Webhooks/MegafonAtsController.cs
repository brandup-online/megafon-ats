using MegafonATS.Models;
using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Models.Webhooks.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;

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

                        HistoryModel model = new()
                        {
                            Type = Enum.Parse<CallDirection>(Request.Form["type"], true),
                            User = Request.Form["user"],
                            UserExt = Request.Form["ext"],
                            GroupRealName = Request.Form["groupRealName"],
                            UserPhone = Request.Form["telnum"],
                            Phone = Request.Form["phone"],
                            Diversion = Request.Form["diversion"],
                            Start = DateTime.ParseExact(Request.Form["start"], "yyyyMMddThhmmssZ", CultureInfo.InvariantCulture),
                            Duration = int.Parse(Request.Form["duration"]),
                            CallId = Request.Form["callid"],
                            Link = new Uri(Request.Form["link"]),
                            Rating = int.Parse(Request.Form["rating"]),
                            Status = Enum.Parse<CallStatus>(Request.Form["status"], true)
                        };

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
                        EventModel model = new()
                        {
                            Type = Enum.Parse<EventType>(Request.Form["type"], true),
                            Phone = Request.Form["phone"],
                            User = Request.Form["user"],
                            UserExt = Request.Form["ext"],
                            GroupRealName = Request.Form["groupRealName"],
                            UserPhone = Request.Form["telnum"],
                            Diversion = Request.Form["diversion"],
                            CallId = Request.Form["callid"],
                            Direction = Enum.Parse<CallDirection>(Request.Form["direction"], true)
                        };

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
                        ContactModel model = new()
                        {
                            CallId = Request.Form["callid"],
                            Phone = Request.Form["phone"],
                            Diversion = Request.Form["diversion"],

                        };
                        ContactResponse response = new();

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
                        RatingModel model = new()
                        {
                            Phone = Request.Form["phone"],
                            Rating = Request.Form["rating"],
                            User = Request.Form["user"],
                            UserExt = Request.Form["ext"],
                            CallId = Request.Form["callid"]
                        };
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