using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace SimpleClinic.Infrastructure.ModelBinders;

/// <summary>
/// Ensures that the date supplied  is parsed to become an Kind.Utc instance
/// </summary>
public class DateTimeModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueProviderResult.FirstValue))
        {
            bool binederSucceeded = false;
            DateTime parsedValue = default;
            try
            {
                string dateStr = valueProviderResult.FirstValue;
                parsedValue = Convert.ToDateTime(dateStr);
                parsedValue = parsedValue.ToUniversalTime();

                binederSucceeded = true;
            }
            catch (Exception ex)
            {

                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
            }

            if (binederSucceeded)
            {
                bindingContext.Result = ModelBindingResult.Success(parsedValue);
            }
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        return Task.CompletedTask;
    }
    
}
