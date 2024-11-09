namespace Dog.Web.Models;

/// <summary>
/// Модель собаки для отображения.
/// </summary>
public class DogDto : DogBaseDto
{
    /// <summary>
    /// Идентификатор собаки.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Возраст собаки.
    /// </summary>
    public int Age { get; set; }
}