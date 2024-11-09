namespace Dog.Web.Models;

/// <summary>
/// Модель с конкатинированными кличкой и именем.
/// </summary>
public class DogConcatNameDto
{
    /// <summary>
    /// Кличка и порода.
    /// </summary>
    public string NameAndKind { get; set; } = string.Empty;
}