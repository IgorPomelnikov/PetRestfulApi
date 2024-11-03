using Dog.App.Models;

namespace Dog.Web.Services;

public class PropertyMappingService : IPropertyMappingService
{
    private readonly Dictionary<string, PropertyMappingValue> _propertyMappingValues =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "Age", new ([nameof(Domain.Dog.DateOfBirth)], true)}
        };

    private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<DogDto, Domain.Dog>(_propertyMappingValues));
    }
    
    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
        var matchingMappings = _propertyMappings
            .OfType<PropertyMapping<TSource, TDestination>>();
        if (matchingMappings.Any())
        {
            return matchingMappings.First().MappingDictionary;
        }
        else
        {
            throw new InvalidOperationException($"No mapping for <{typeof(TSource)},{typeof(TDestination)}>");
        }
    }
}