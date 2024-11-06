using Contracts;

namespace Dog.Web.Models;

public class DogWithToyDto : DogBaseDto
{
    public PetsToyDto Toy { get; set; }
}