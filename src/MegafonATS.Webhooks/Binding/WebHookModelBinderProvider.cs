using MegafonATS.Models.Webhooks.Binding;
using MegafonATS.Webhooks.Models.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MegafonATS.Webhooks.Binding
{
    public class WebHookModelBinderProvider : IModelBinderProvider
    {
        readonly static Type[] ModelTypes = new[] { typeof(HistoryModel), typeof(RatingModel), typeof(EventModel), typeof(ContactModel) };

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(WebHookModel))
                return null;

            var binders = new Dictionary<string, (ModelMetadata, IModelBinder)>();
            foreach (var type in ModelTypes)
            {
                var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
                binders[type.Name] = (modelMetadata, context.CreateBinder(modelMetadata));
            }

            return new WebHookModelBinder(binders);
        }
    }
}