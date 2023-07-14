using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace SimpleClinic.Infrastructure.ModelBinders;

/// <summary>
/// DateTime custom model binder
/// </summary>
public class DateTimeModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (result != ValueProviderResult.None && !string.IsNullOrWhiteSpace(result.FirstValue))
        {
            DateTime dateTime;
            var value = result.FirstValue;

            if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                bindingContext.Result = ModelBindingResult.Success(dateTime);
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid date format");
            }
        }

        return Task.CompletedTask;
    }
}
