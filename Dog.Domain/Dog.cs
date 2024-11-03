using System.ComponentModel.DataAnnotations;

namespace Dog.Domain;

public class Dog
{
    [Required]
    public int DogId { get; set; }
    
    [Required]
    [MaxLength(25)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Kind { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }
}