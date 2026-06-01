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

            if (string.IsNullOrEmpty(cmd))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Поле cmd обязательно.");
                return;
            }

            var key = cmd[..1].ToUpper() + cmd[1..] + "Model";

            if (!binders.TryGetValue(key, out var binderEntry))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"Неизвестная команда: {cmd}");
                return;
            }

            var (modelMetadata, modelBinder) = binderEntry;

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
                if (bindingContext.Result.Model is not HistoryModel history)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Не удалось получить модель history.");
                    return;
                }

                var start = bindingContext.ValueProvider.GetValue("start").FirstValue;

                if (DateTime.TryParseExact(start, "yyyyMMddTHHmmssZ", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal, out var startDate))
                {
                    history.Start = startDate;
                    bindingContext.ModelState.SetModelValue("Start", history.Start, start);
                    bindingContext.ModelState["Start"].ValidationState = ModelValidationState.Unvalidated;
                }
                else
                {
                    bindingContext.ModelState.TryAddModelError("Start", $"Не удалось преобразовать строку '{start}' в валидную дату.");
                }
            }
        }
    }
}
