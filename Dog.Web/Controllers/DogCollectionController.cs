using Dog.App.Repositories;
using Dog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dog.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DogCollectionController(IDogRepository repository) : ControllerBase
{
    private readonly IDogRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    [HttpPost]
    public async Task<IActionResult> CreateDogs(IEnumerable<DogCreateDto> dogs)
    {
        throw new NotImplementedException();
    }
}