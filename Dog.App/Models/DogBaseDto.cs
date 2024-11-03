using System.ComponentModel.DataAnnotations;

namespace Dog.App.Models;

public abstract class DogBaseDto
{
    [Required]
    public virtual string Name { get; set; } = string.Empty;
    
    [Required]
    public string Kind { get; set; } = string.Empty;
}