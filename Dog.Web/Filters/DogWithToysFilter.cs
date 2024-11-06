using AutoMapper;
using Dog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dog.Web.Filters;

public class DogWithToysFilter(IMapper mapper) : IAsyncResultFilter
{

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var resultFromAction = context.Result as ObjectResult;
        if (resultFromAction?.Value == null
            || context.HttpContext.Response.StatusCode < 200
            || context.HttpContext.Response.StatusCode >= 300
           )
        {
            await next();
            return;
        }
        
        var (dog, toy) = ((Domain.Dog, Contracts.PetsToyDto))resultFromAction.Value;
        var dogDto = mapper.Map<DogWithToyDto>(dog);
        resultFromAction.Value = mapper.Map(mapper.Map<PetsToyDto>(toy), dogDto);
        await next();
    }
}