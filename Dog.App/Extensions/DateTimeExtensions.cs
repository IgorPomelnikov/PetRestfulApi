namespace Dog.App.Extensions;

public static class DateTimeExtensions
{
    public static int GetAge(this DateTime dateTime)
    {
        return new DateTime().AddDays((DateTime.Now - dateTime).Days).Year;
    }
}