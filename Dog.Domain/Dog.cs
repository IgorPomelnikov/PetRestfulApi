using System.ComponentModel.DataAnnotations;

namespace Dog.Domain;

/// <summary>
/// Базовый класс собаки.
/// </summary>
public class Dog
{
    /// <summary>
    /// Уникальный идентификатор пса.
    /// </summary>
    [Required]
    public int DogId { get; set; }
    
    
    /// <summary>
    /// Кличка собаки.
    /// </summary>
    [Required]
    [MaxLength(25)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Порода собаки.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string Kind { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения пёсиля.
    /// </summary>
    [Required]
    public DateTime DateOfBirth { get; set; }
}