using System.ComponentModel.DataAnnotations;
using Dog.App.Attributes;

namespace Dog.App.Models;

[NameHasToBeDifferentFromKind]
public abstract class DogBaseDto
{
    [Required]
    public virtual string Name { get; set; } = string.Empty;
    
    [Required]
    public string Kind { get; set; } = string.Empty;
}