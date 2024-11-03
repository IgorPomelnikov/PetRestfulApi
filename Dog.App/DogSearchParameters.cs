namespace Dog.App;

public class DogSearchParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    public string Filter { get; set; }
    public string Search { get; set; }
    public string Group { get; set; }
    public string Sort { get; set; }
    public int Page { get; set; }
    public int PageSize
    {
        get=> _pageSize;
        set
        {
            _pageSize = value switch
            {
                > MaxPageSize => MaxPageSize,
                < 1 => 1,
                _ => value
            };
        }
    }
}