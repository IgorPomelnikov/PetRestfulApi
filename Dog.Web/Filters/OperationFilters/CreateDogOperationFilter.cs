using Dog.Web.Heplers;
using Dog.Web.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dog.Web.Filters.OperationFilters;

public class CreateDogOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if(operation.OperationId != OperationNames.CreateDog)
            return;
        var schema = context.SchemaGenerator.GenerateSchema(typeof(DogShepherdCrateDto), context.SchemaRepository);
        operation.RequestBody
            .Content.Add(SupportedMediaTypes.VendorDogCreateShepherdJson, new OpenApiMediaType(){Schema = schema});
    }
}