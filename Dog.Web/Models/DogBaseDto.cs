using System.ComponentModel.DataAnnotations;
using Dog.Web.Attributes;

namespace Dog.Web.Models;

[NameHasToBeDifferentFromKind]
public abstract class DogBaseDto
{
    [Required]
    public virtual string Name { get; set; } = string.Empty;
    
    [Required]
    public string Kind { get; set; } = string.Empty;
}