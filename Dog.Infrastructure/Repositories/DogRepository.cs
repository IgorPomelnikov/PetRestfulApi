﻿using System.Text.Json;
using Contracts;
using Dog.App;
using Dog.App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dog.Infrastructure.Repositories;

public class DogRepository(DogContext context, IHttpClientFactory httpClient) : IDogRepository
{
    public async Task<Domain.Dog?> GetDog(int id)
    {
        var dog = await context.Dogs.FindAsync(id);
        return dog;
    }

    public async Task<IEnumerable<Domain.Dog>> GetDogs(DogSearchParameters parameters)
    {
        var dogs = await context.Dogs.Where(x => x.Name == parameters.Filter || x.Name.Contains(parameters.Search))
            .ToListAsync();
        return dogs;
    }

    public async Task<PetsToyDto?> GetPetsToy(int dogId)
    {
        var client = httpClient.CreateClient();
        var response = await client.GetAsync($"http://localhost:5009/api/toys/{dogId}");
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<PetsToyDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        return null;
    }

    public async Task<PetsToyDto?> GetDogWithToy(int dogId)
    {
        var client = httpClient.CreateClient();
        var response = await client.GetAsync($"http://localhost:5009/api/toys/{dogId}");
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<PetsToyDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        return null;
    }

    public async Task<int> CreateDog(Domain.Dog dog)
    {
        context.Dogs.Add(dog);
        return await context.SaveChangesAsync();
    }

    public IAsyncEnumerable<Domain.Dog> GetAllDogs()
    {
        return context.Dogs.AsNoTracking().AsAsyncEnumerable<Domain.Dog>(); 
    }

    public async Task<bool> UpdateDog(Domain.Dog dog)
    {
        context.Entry(dog).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}