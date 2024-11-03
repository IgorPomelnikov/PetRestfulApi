namespace Dog.Web.Services;

public class PropertyMappingValue
{
    public IEnumerable<string> DestinationProperties { get; private set; }
    public bool Revert { get; private set; }

    public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
    {
        ArgumentNullException.ThrowIfNull(destinationProperties);
        DestinationProperties = destinationProperties;
        Revert = revert;
    }
    
}