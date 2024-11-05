using Dog.Web.Filters;
using Dog.Web.Heplers;
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
    [TypeFilter(typeof(DogResultFilter))]
    public async Task<IActionResult> GetDog(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [HttpHead]
    [TypeFilter(typeof(DogsResultFilter))]
    public async Task<IActionResult> GetDogs()
    {
        throw new NotImplementedException();
    }
    [HttpGet("({bulkIds})", Name = "GetDogsCollection")]
    [HttpHead]
    public async Task<IActionResult> GetBulkDogs([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<int> bulkIds)
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