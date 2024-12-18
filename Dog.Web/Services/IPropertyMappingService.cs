namespace Dog.Web.Services;

public interface IPropertyMappingService
{
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
}