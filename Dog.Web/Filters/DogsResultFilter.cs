using AutoMapper;
using Dog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dog.Web.Filters;

public class DogsResultFilter(IMapper mapper) : IAsyncResultFilter
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

        result.Value = _mapper.Map<IEnumerable<DogDto>>(result.Value);
        
        await next();
    }
}