using Dog.Web.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Dog.Web.Controllers;

[ApiController]
[Route("api/dogs")]
public class DogsController : ControllerBase
{
    [HttpGet("{id:int}", Name = "GetDog")]
    [HttpHead]
    public async Task<IActionResult> GetDog(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [HttpHead]
    public async Task<IActionResult> GetDogs()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDog(DogCreateDto dog)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateDog(int id, DogUpdateDto dog)
    {
        throw new NotImplementedException();
    }
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateDog(int id, JsonPatchDocument<DogUpdateDto> dog)
    {
        throw new NotImplementedException();
    }
    
    [HttpOptions]
    public async Task<IActionResult> GetOptions()
    {
        throw new NotImplementedException();
    }

    private IEnumerable<LinkDto> GetLinks()
    {
        throw new NotImplementedException();
    }
}