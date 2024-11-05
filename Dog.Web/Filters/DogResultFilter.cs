using AutoMapper;
using Dog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dog.Web.Filters;

public class DogResultFilter(IMapper mapper) : IAsyncResultFilter
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;
        if (result?.Value == null ||
            result.StatusCode < 200 ||
            result.StatusCode >= 300)
        {
            await next();
            return;
        }
        // здесь можно получить данные об action, например, получить атрибуты, и по ним добавлять ссылки hateoas
        result.Value = _mapper.Map<DogDto>(result.Value);
        
        await next();
    }
}