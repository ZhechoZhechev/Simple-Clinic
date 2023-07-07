namespace SimpleClinic.Infrastructure.ModelBinders;

using System.Globalization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// Model binder for decimal vlues
/// </summary>
public class DecimaModelBinder : IModelBinder
{
    /// <summary>
    /// Method from ImodelBinder interface
    /// </summary>
    /// <param name="bindingContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        ValueProviderResult result =
            bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (result != ValueProviderResult.None && !string.IsNullOrEmpty(result.FirstValue))
        {
            decimal parsedValue = 0m;
            bool binederSucceeded = false;

            try
            {
                string formDecValue = result.FirstValue;
                formDecValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                formDecValue.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

                parsedValue = Convert.ToDecimal(formDecValue);
                binederSucceeded = true;
            }
            catch (FormatException fe)
            {

                bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
            }

            if (binederSucceeded) 
            {
                bindingContext.Result = ModelBindingResult.Success(parsedValue);
            }

        }
            return Task.CompletedTask;
    }
}
