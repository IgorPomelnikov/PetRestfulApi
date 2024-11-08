namespace PetToysApi;

public class BadDogFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var id = context.GetArgument<int>(0);
        if (id == 4)
            return TypedResults.NotFound("This dog is in our blacklist, so it wouldn't get a toy!");
        return await next(context);
    }
}