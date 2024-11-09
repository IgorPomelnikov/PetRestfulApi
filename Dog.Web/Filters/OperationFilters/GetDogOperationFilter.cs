using Dog.Web.Heplers;
using Dog.Web.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dog.Web.Filters.OperationFilters;

/// <summary>
/// Фильтр, который добавляет к генерации документации OpenApi дополнительно
/// поддерживаемые результирующие форматы данных.
/// Это нужно для того, чтобы если на одинаковых по  роутингу методах возвращаются разные типы данных.
/// </summary>
/// <remarks>
/// NB: Не забудьте отключить экспортирование в спецификацию дублирующие(в плане маршрута)
/// методы применив к ним атрибут [ApiExplorerSettings(IgnoreApi = true)]
/// NB: Не забудьте создать атрибут, реализующий IActionConstraint, для того, чтобы сделать маршруты однозначными для
/// системы.
/// </remarks>
internal class GetDogOperationFilter : IOperationFilter
{
    ///<inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //Применяем только к определённому Name(то, что указываем в Route) метода.
        if(operation.OperationId != OperationNames.GetDog)
            return;
        
        //Генерируем схему (по спецификации OpenApi, описание классов внизу страницы swagger) для определённого объекта. 
        var schema = context.SchemaGenerator.GenerateSchema(typeof(DogConcatNameDto), context.SchemaRepository);
        
        //К ответу со статусом 200 добавляем новый Media Type и схему для этого объекта.
        operation.Responses[StatusCodes.Status200OK.ToString()]
            .Content.Add(SupportedMediaTypes.VendorDogConcatJson,new OpenApiMediaType(){Schema = schema});
    }
}