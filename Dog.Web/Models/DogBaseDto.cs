using System.ComponentModel.DataAnnotations;
using Dog.Web.Attributes;

namespace Dog.Web.Models;


/// <summary>
/// Абстрактная модель объекта собаки.
/// </summary>
[NameHasToBeDifferentFromKind]
public abstract class DogBaseDto
{
    
    /// <summary>
    /// Кличка собаки.
    /// </summary>
    [Required]
    public virtual string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Порода собаки.
    /// </summary>
    [Required]
    public string Kind { get; set; } = string.Empty;
}