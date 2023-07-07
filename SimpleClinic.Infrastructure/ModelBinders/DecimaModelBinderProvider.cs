namespace SimpleClinic.Infrastructure.ModelBinders;

using Microsoft.AspNetCore.Mvc.ModelBinding;
/// <summary>
/// 
/// </summary>
public class DecimaModelBinderProvider : IModelBinderProvider
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

        if (context.Metadata.ModelType == typeof(decimal) ||
            context.Metadata.ModelType == typeof(decimal?)) 
        {
            return new DecimaModelBinder();
        }

        return null;
    }
}
