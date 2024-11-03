using System.ComponentModel.DataAnnotations;

namespace Dog.App.Models;

public class DogCreateDto : DogBaseDto
{
    [Required]
    public DateTime DateOfBirth { get; set; }
}