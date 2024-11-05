using System.ComponentModel.DataAnnotations;

namespace Dog.Web.Models;

public class DogUpdateDto : DogBaseDto
{
    [Required]
    public DateTime DateOfBirth { get; set; }
    
}