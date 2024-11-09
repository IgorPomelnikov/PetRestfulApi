using AutoMapper;
using Dog.App.Repositories;
using Dog.Web.Attributes;
using Dog.Web.Filters;
using Dog.Web.Heplers;
using Dog.Web.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Dog.Web.Controllers;

/// <summary>
/// Контроллер работы с собаками.
/// </summary>
/// <param name="repository">Репозиторий собак.</param>
/// <param name="mapper">Маппер для собак.</param>
[ApiController]
[Route("api/dogs")]
[Produces(SupportedMediaTypes.Json, SupportedMediaTypes.Xml)]
public class DogsController(IDogRepository repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получение собаки по её id.
    /// </summary>
    /// <param name="id">Id собаки.</param>
    /// <returns>Собака в Json формате.</returns>
    [HttpGet("{id:int}", Name = OperationNames.GetDog)]
    [TypeFilter(typeof(DogResultFilter))]
    [RequestHeaderMatchesMediaType("Accept", SupportedMediaTypes.Json, SupportedMediaTypes.VendorDogJson)]
    [Produces(SupportedMediaTypes.VendorDogJson)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DogDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDog(int id)
    {
        var dog = await repository.GetDog(id);
        if(dog == null)
            return NotFound();
        return Ok(dog);
    }
    
    
    /// <summary>
    /// Получение собаки по её id.
    /// </summary>
    /// <param name="id">Id собаки.</param>
    /// <returns>Собака в Json формате.</returns>
    [HttpGet("{id:int}", Name = OperationNames.GetDog)]
    [RequestHeaderMatchesMediaType("Accept", SupportedMediaTypes.VendorDogConcatJson)]
    [Produces(SupportedMediaTypes.VendorDogConcatJson)]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(DogConcatNameDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> GetDogWithConcatNameAndKind(int id)
    {
        var dog = await repository.GetDog(id);
        if(dog == null)
            return NotFound();
        var dogDto = mapper.Map<DogConcatNameDto>(dog);
        return Ok(dogDto);
    }

    /// <summary>
    /// Получение пёсика с его игрушкой.
    /// </summary>
    /// <param name="dogId">Id пёсика.</param>
    /// <param name="cancellationToken">Токен для отмены запроса.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("/{dogId:int}/withtoy", Name = OperationNames.GetDogWithToy)]
    [TypeFilter(typeof(DogWithToysFilter))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetDogWithToy(int dogId, CancellationToken cancellationToken)
    {
        var dogEntity = await repository.GetDog(dogId);
        if(dogEntity == null)
            return NotFound();
        var toy = await repository.GetPetsToy(dogId, cancellationToken);
        return Ok((dogEntity, toy));
        
    }
    
    // [HttpGet]
    // [HttpHead]
    // [TypeFilter(typeof(DogsResultFilter))]
    // public async Task<IActionResult> GetDogs()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [Route("({bulkIds})", Name = "GetDogsCollection")]
    // [HttpGet]
    // [HttpHead]
    // public async Task<IActionResult> GetBulkDogs([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<int> bulkIds)
    // {
    //     throw new NotImplementedException();
    // }

    [HttpGet("dogstream")]
    public async IAsyncEnumerable<DogDto> GetDogsStream()
    {
        await foreach (var dog in repository.GetAllDogs())
        {
            await Task.Delay(100);
            yield return mapper.Map<DogDto>(dog);
        }
    }
    
    [HttpPost(Name = OperationNames.CreateDog)]
    [RequestHeaderMatchesMediaType("Content-Type", 
        SupportedMediaTypes.Json, 
        SupportedMediaTypes.VendorDogJson, 
        SupportedMediaTypes.Xml)]
    [Consumes(
        SupportedMediaTypes.Json, 
        SupportedMediaTypes.VendorDogJson, 
        SupportedMediaTypes.Xml)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateDog(DogCreateDto dog)
    {
        var dogEntity = mapper.Map<Domain.Dog>(dog);
        var val = await repository.CreateDog(dogEntity);
        var created = mapper.Map<DogDto>(dogEntity);
        return CreatedAtRoute(OperationNames.GetDog, new {dogId = val}, created );
    }
    
    [HttpPost(Name = OperationNames.CreateDog)]
    [RequestHeaderMatchesMediaType("Content-Type", SupportedMediaTypes.VendorDogCreateShepherdJson)]
    [Consumes( SupportedMediaTypes.VendorDogCreateShepherdJson)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> CreateShepherdDog(DogShepherdCrateDto dog)
    {
        var dogEntity = mapper.Map<Domain.Dog>(dog);
        var val = await repository.CreateDog(dogEntity);
        var created = mapper.Map<DogDto>(dogEntity);
        return CreatedAtRoute(OperationNames.GetDog, new {dogId = val}, created );
    }
    //
    //
    // [HttpPut("{id:int}")]
    // public async Task<IActionResult> UpdateDog(int id, DogUpdateDto dog)
    // {
    //     throw new NotImplementedException();
    // }
    // [HttpPatch("{id:int}")]
    // public async Task<IActionResult> UpdateDog(int id, JsonPatchDocument<DogUpdateDto> dog)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpOptions]
    // public async Task<IActionResult> GetOptions()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // private IEnumerable<LinkDto> GetLinks()
    // {
    //     throw new NotImplementedException();
    // }
}