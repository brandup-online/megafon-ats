using MefafonATS.Model.WebhooksModel;
using Microsoft.AspNetCore.Mvc;

namespace MefafonATS.Webhooks
{
    [Route("megafon")]
    [ApiController]
    public class MegafonAtsController : Controller
    {
        readonly IMegafonAtsEvents _megafonAtsEvents;
        public MegafonAtsController(IMegafonAtsEvents megafonAtsEvents)
        {
            _megafonAtsEvents = megafonAtsEvents;
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
                        await _megafonAtsEvents.HistoryAsync(model);
                        return Ok("history");
                    }
                case "event":
                    {
                        EventModel model = new();
                        _ = await TryUpdateModelAsync(model);
                        await _megafonAtsEvents.EventAsync(model);
                        return Ok("event");
                    }
                case "contact":
                    {
                        ContactModel model = new();
                        _ = await TryUpdateModelAsync(model);
                        await _megafonAtsEvents.ContactAsync(model);
                        return Ok("contact");
                    }
                case "rating":
                    {
                        RatingModel model = new();
                        _ = await TryUpdateModelAsync(model);
                        await _megafonAtsEvents.RatingAsync(model);
                        return Ok("rating");
                    }
                default:
                    return BadRequest();
            }





        }

    }

}