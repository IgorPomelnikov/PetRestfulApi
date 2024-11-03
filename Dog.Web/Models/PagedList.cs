namespace Dog.Web.Models;

public class PagedList<T> : List<T>
{
    public int Page { get; set; }
    public int PageCount { get; set; }
    public int Total { get; set; }
}