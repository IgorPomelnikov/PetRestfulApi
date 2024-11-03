using System.ComponentModel.DataAnnotations;
using Dog.App.Models;

namespace Dog.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class NameHasToBeDifferentFromKindAttribute
    : ValidationAttribute
{
    public NameHasToBeDifferentFromKindAttribute()
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is DogBaseDto dogBaseDto)
        {
            return dogBaseDto.Name.Equals(dogBaseDto.Kind, StringComparison.OrdinalIgnoreCase) 
                ? new ValidationResult(
                    $"{nameof(dogBaseDto.Kind)} and {nameof(dogBaseDto.Name)} must be different!",
                    new[] { nameof(dogBaseDto.Name) }) 
                : ValidationResult.Success;
        }
        else
        {
            throw new Exception($"The {nameof(NameHasToBeDifferentFromKindAttribute)} is applicable only to {nameof(DogBaseDto)}.");
        }
    }
}