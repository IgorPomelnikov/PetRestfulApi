using System.Reflection;
using System.Text.Json.Serialization;
using Dog.App.Repositories;
using Dog.Infrastructure;
using Dog.Infrastructure.Repositories;
using Dog.Web.Filters;
using Dog.Web.Filters.OperationFilters;
using Dog.Web.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("DogApiDocs", new()
    {
        Title = "Dog API",
        Version = "1.0.0",
        Description = "A simple example ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "Dog",
            Email = "dog@dog.com",
            Url = new Uri("https://dog.com")
        },
        License = new OpenApiLicense
        {
            Name = "It is not recommended for cats",
            Url = new Uri("https://dog.com")
        }
        
    });
    // setupAction.ResolveConflictingActions(apiDescriptions =>
    // {
    //     // Решение конфликта "в лоб". Плохое. Используй IOperationFilter
    //     return apiDescriptions.First();
    // });
    setupAction.OperationFilter<GetDogOperationFilter>();
    setupAction.OperationFilter<CreateDogOperationFilter>();
    var docFile = Path.Combine(AppContext.BaseDirectory,$"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    
    setupAction.IncludeXmlComments(docFile);
});
#region Services
builder.Services.AddControllers(configure =>
    {
        
        configure.ReturnHttpNotAcceptable = true;
        configure.CacheProfiles.Add("240SecondsCacheProfile",
            new() { Duration = 240 });
    }).AddNewtonsoftJson(setupAction => //из-за этого не работает iasyncEnumerable
    {
        setupAction.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();
    })
    .AddXmlDataContractSerializerFormatters()
    .ConfigureApiBehaviorOptions(setupAction =>
    {
        setupAction.InvalidModelStateResponseFactory = context =>
        {
            var factory = context.HttpContext.RequestServices
                .GetRequiredService<ProblemDetailsFactory>();
            var validationProblemDetails = factory.CreateValidationProblemDetails(
                context.HttpContext, context.ModelState);
            validationProblemDetails.Detail =
                "See the errors property for more details.";
            validationProblemDetails.Instance = context.HttpContext.Request.Path;
            validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
            validationProblemDetails.Title = "One or more validation errors occurred.";
            return new UnprocessableEntityObjectResult(validationProblemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });
builder.Services.Configure<MvcOptions>(configure =>
{
    // You have to configure this only after connection newtonsoft formatter to controllers.
    var newtonsoftJsonOutputFormatter = configure.OutputFormatters
        .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
    if (newtonsoftJsonOutputFormatter != null)
    {
        newtonsoftJsonOutputFormatter.SupportedMediaTypes
            .Add("application/vnd.igor.hateoas+json");
    }
});
builder.Services.AddTransient<IPropertyMappingService, PropertyMappingService>();
builder.Services.AddDbContext<DogContext>(options =>
{
    options.UseSqlite(@"Data Source=Dog.db");
});

var ctx = builder.Services.BuildServiceProvider().GetRequiredService<DogContext>();
ctx.Database.EnsureCreated();

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddResponseCaching();

builder.Services.AddHttpCacheHeaders(
    (expirationModelOptions) =>
    {
        expirationModelOptions.MaxAge = 60;
        expirationModelOptions.CacheLocation = 
            Marvin.Cache.Headers.CacheLocation.Private;
    },
    (validationModelOptions) =>
    {
        validationModelOptions.MustRevalidate = true;
    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IDogRepository, DogRepository>();
builder.Services.AddHttpClient();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        setupAction.SwaggerEndpoint("/swagger/DogApiDocs/swagger.json", "Dog API");
        setupAction.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

