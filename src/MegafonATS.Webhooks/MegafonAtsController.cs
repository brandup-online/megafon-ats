using MegafonATS.Models.Webhooks;
using Microsoft.AspNetCore.Mvc;

namespace MegafonATS.Webhooks
{
    [Route("megafon")]
    [ApiController]
    public class MegafonAtsController : Controller
    {
        readonly IMegafonAtsEvents megafonAtsEvents;

        public MegafonAtsController(IMegafonAtsEvents megafonAtsEvents)
        {
            this.megafonAtsEvents = megafonAtsEvents ?? throw new ArgumentNullException(nameof(megafonAtsEvents));
        }

        [HttpPost("callback")]
        public async Task<IActionResult> Command()
        {
            var cmd = Request.Form["cmd"];
            var token = Request.Form["token"];
            if (token == "") return BadRequest("token");
            switch (cmd)
            {
                case "history":
                    {
                        HistoryModel model = new();
                        _ = await TryUpdateModelAsync(model);
                        _ = DateTime.TryParseExact(Request.Form["start"],
                                                "yyyyMMddThhmmss",
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.None,
                                                out DateTime tmp);
                        model.Start = tmp;
                        await megafonAtsEvents.HistoryAsync(model, token);
                        return Ok("history");
                    }
                case "event":
                    {
                        EventModel model = new();
                        _ = await TryUpdateModelAsync(model);
                        await megafonAtsEvents.EventAsync(model, token);
                        return Ok("event");
                    }
                case "contact":
                    {
                        ContactModel model = new();
                        _ = await TryUpdateModelAsync(model);
                        await megafonAtsEvents.ContactAsync(model, token);
                        return Ok("contact");
                    }
                case "rating":
                    {
                        RatingModel model = new();
                        _ = await TryUpdateModelAsync(model);
                        await megafonAtsEvents.RatingAsync(model, token);
                        return Ok("rating");
                    }
                default:
                    return BadRequest();
            }
        }
    }
}