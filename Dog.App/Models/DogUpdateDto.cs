using System.ComponentModel.DataAnnotations;

namespace Dog.App.Models;

public class DogUpdateDto : DogBaseDto
{
    [Required]
    public DateTime DateOfBirth { get; set; }
    
}