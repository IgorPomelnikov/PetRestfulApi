using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dog.Web.Heplers;

public class ArrayModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (!bindingContext.ModelMetadata.IsEnumerableType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }
        
        var typeOfElements = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
        var converter = TypeDescriptor.GetConverter(typeOfElements);
        
        var values = value.Split(',',StringSplitOptions.RemoveEmptyEntries).Select(x=>converter.ConvertFromString(x)).ToArray();
        var typedValues = Array.CreateInstance(typeOfElements, values.Length);
        values.CopyTo(typedValues, 0);
        
        bindingContext.Result = ModelBindingResult.Success(typedValues);
        return Task.CompletedTask;
        
    }
}