using Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using PetToysApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication().AddJwtBearer(); //paste to the terminal: dotnet user-jwts create --audience "toy-api" --role Admin --claim "TestClaim=yes"

builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("TestPolicy",policy => 
        policy
            .RequireRole("Admin")
            .RequireClaim("TestClaim", "yes"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var toys = new List<PetsToyDto>()
{
    new() { OwnerId = 1, Name = "Ball" },
    new() { OwnerId = 2, Name = "Bone" },
    new() { OwnerId = 3, Name = "Stick" },
    new() { OwnerId = 4, Name = "Donout" },
};
var api = app.MapGroup("/api");
api.MapGet("/toys/{id:int}", async Task<Results<NotFound, Ok<PetsToyDto>>>(int id) =>
    {
        await Task.Delay(500);
        var result = toys.FirstOrDefault(x => x.OwnerId == id);
        if(result == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(result);
    })
    .WithName("GetToysForPet")
    .WithOpenApi()
    .AddEndpointFilter<BadDogFilter>();

api.MapPost("/toys", async Task<CreatedAtRoute<PetsToyDto>> (PetsToyDto petToyDto) =>
{
    await Task.Delay(500);
    toys.Add(petToyDto);
    return TypedResults.CreatedAtRoute(petToyDto, "GetToysForPet", new{id=petToyDto.OwnerId});
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/test-jwt", () =>
    {
        return TypedResults.Ok("ok");
    }).RequireAuthorization("testPolicy")
    .WithOpenApi(opt =>
    {
        opt.Deprecated = true;
        opt.Parameters = new List<OpenApiParameter>(){new OpenApiParameter(){Name = "param", Description = "test desription"}};
        return opt;
    })
    .ProducesProblem(StatusCodes.Status403Forbidden);
app.Run();

