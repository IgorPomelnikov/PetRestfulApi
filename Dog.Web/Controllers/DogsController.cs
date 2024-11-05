using AutoMapper;
using Dog.App.Repositories;
using Dog.Web.Filters;
using Dog.Web.Heplers;
using Dog.Web.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Dog.Web.Controllers;

[ApiController]
[Route("api/dogs")]
public class DogsController(IDogRepository repository, IMapper mapper) : ControllerBase
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

    [HttpGet("dogstream")]
    public async IAsyncEnumerable<DogDto> GetDogsStream()
    {
        var dogsEnumAsync = repository.GetAllDogs();
        await foreach (var dog in dogsEnumAsync)
        {
            await Task.Delay(300);
            yield return mapper.Map<DogDto>(dog);
        }
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