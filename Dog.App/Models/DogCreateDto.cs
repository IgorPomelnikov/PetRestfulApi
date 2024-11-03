using System.ComponentModel.DataAnnotations;

namespace Dog.App.Models;

public class DogCreateDto : DogBaseDto, IValidatableObject
{
    [Required]
    public DateTime DateOfBirth { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var lowestBorder  = DateTime.Now.AddYears(-40);
        if (DateOfBirth < lowestBorder)
        {
            yield return new ValidationResult($"Date of birth must be more than {lowestBorder:yyyy}.", new[] { nameof(DateOfBirth) });
        }
    }
}