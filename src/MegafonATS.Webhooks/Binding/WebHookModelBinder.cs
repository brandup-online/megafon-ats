using MegafonATS.Webhooks.Models.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace MegafonATS.Models.Webhooks.Binding
{
    public class WebHookModelBinder : IModelBinder
    {
        readonly Dictionary<string, (ModelMetadata, IModelBinder)> binders;

        public WebHookModelBinder(Dictionary<string, (ModelMetadata, IModelBinder)> binders)
        {
            this.binders = binders ?? throw new ArgumentNullException(nameof(binders));
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var cmd = bindingContext.ValueProvider.GetValue("cmd").FirstValue;

            IModelBinder modelBinder;
            ModelMetadata modelMetadata;

            (modelMetadata, modelBinder) = binders[cmd[..1].ToUpper() + cmd[1..] + "Model"];

            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
                                                            bindingContext.ActionContext,
                                                            bindingContext.ValueProvider,
                                                            modelMetadata,
                                                            bindingInfo: null,
                                                            bindingContext.ModelName);

            await modelBinder.BindModelAsync(newBindingContext);
            bindingContext.Result = newBindingContext.Result;

            if (cmd == "history")
            {

                var history = bindingContext.Result.Model as HistoryModel;
                var start = bindingContext.ValueProvider.GetValue("start").FirstValue;

                if (DateTime.TryParseExact(start, "yyyyMMddTHHmmssZ", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal, out var startDate))
                    history.Start = startDate;
                else
                {
                    throw new Exception($"Не удалось преобразавать строку {start} в валидную дату");
                }

                bindingContext.ModelState.SetModelValue("Start", history.Start, start);
                bindingContext.ModelState["Start"].ValidationState = ModelValidationState.Unvalidated;
            }
        }
    }
}
