using Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapGet("/api/toys/{id:int}", (int id) =>
    {
        return toys.First(x => x.OwnerId == id);
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

