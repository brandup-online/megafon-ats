using MegafonATS.Models.Attributes;
using MegafonATS.Models.Webhooks.Requests;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace MegafonATS.Webhooks.Helpers
{
    internal static class ModelMapper
    {
        public static bool MapAndValidate(IFormCollection form, out IWebHookModel model)
        {
            model = MapModel(form);

            return ValidateModel(model);
        }
        public static bool ValidateModel(IWebHookModel model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, context, validationResults);
        }

        public static IWebHookModel MapModel(IFormCollection form)
        {
            var command = FirstToUpper(form["cmd"]);
            var model = CreateInstance(command + "Model");

            var data = form.ToDictionary(x => x.Key, x => x.Value.ToString());
            data.Remove("cmd");
            data.Remove("crm_token");

            foreach (var property in model.GetType().GetProperties())
            {
                string propertyName = GetPropertyName(property);

                string value;
                if (data.ContainsKey(propertyName))
                    value = data[propertyName];
                else continue;

                if (property.PropertyType == typeof(DateTime))
                {
                    var date = DateTime.ParseExact(value, "yyyy-MM-ddThh:mm:ssZ", CultureInfo.InvariantCulture).ToUniversalTime();
                    property.SetValue(model, date);
                }
                else
                {
                    var converter = TypeDescriptor.GetConverter(property.PropertyType);
                    property.SetValue(model, converter.ConvertFromString(value));
                }
            }

            return model as IWebHookModel;
        }

        #region Helpers

        private static string GetPropertyName(PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<MapNameAttribute>();
            if (attr != null)
                return attr.Name;
            else
                return FirstToLower(property.Name);
        }

        static string FirstToUpper(string text) =>
            text[..1].ToUpper() + text[1..];

        static string FirstToLower(string text) =>
            text[..1].ToLower() + text[1..];

        static object CreateInstance(string className)
        {
            var assembly = Assembly.GetAssembly(typeof(IWebHookModel));

            var type = assembly.GetTypes()
                .First(t => t.Name == className);

            return Activator.CreateInstance(type);
        }

        #endregion
    }
}
