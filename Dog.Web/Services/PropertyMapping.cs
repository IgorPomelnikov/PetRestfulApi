namespace Dog.Web.Services;

public class PropertyMapping<TSource, TDest> : IPropertyMapping
{
    public Dictionary<string, PropertyMappingValue> MappingDictionary { get; set; }

    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        MappingDictionary = mappingDictionary;
    }
}