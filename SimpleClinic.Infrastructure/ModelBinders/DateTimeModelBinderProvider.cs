namespace SimpleClinic.Infrastructure.ModelBinders;

using Microsoft.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// Provider for the date time model binder
/// </summary>
public class DateTimeModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// ImodelbinderProvider interface implementation method
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(DateTime) ||
            context.Metadata.ModelType == typeof(DateTime?))
        {
            return new DateTimeModelBinder();
        }

        return null;
    }
}
