using MegafonATS.Models.Webhooks.Binding;
using MegafonATS.Models.Webhooks.RequestModels;
using MegafonATS.Models.Webhooks.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MegafonATS.Webhooks.Binding
{
    public class WebHookModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(WebHookModel))
            {
                return null;
            }

            var subclasses = new[] { typeof(HistoryModel), typeof(RatingModel), typeof(EventModel), typeof(ContactModel) };

            var binders = new Dictionary<string, (ModelMetadata, IModelBinder)>();
            foreach (var type in subclasses)
            {
                var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
                binders[type.Name] = (modelMetadata, context.CreateBinder(modelMetadata));
            }

            return new WebHookModelBinder(binders);
        }
    }
}
