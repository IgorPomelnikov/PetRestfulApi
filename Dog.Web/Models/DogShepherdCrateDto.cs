using System.ComponentModel.DataAnnotations;

namespace Dog.Web.Models;

/// <summary>
/// Специальная модель для создания именно овчарок.
/// </summary>
public class DogShepherdCrateDto
{
    /// <summary>
    /// Кличка.
    /// </summary>
    [Required]
    public required string Name { get; set; }
    /// <summary>
    /// Дата рождения.
    /// </summary>
    [Required]
    public required DateTime DateOfBirth { get; set; }
}