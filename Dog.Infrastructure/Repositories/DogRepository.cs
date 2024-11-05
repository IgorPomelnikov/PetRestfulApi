using Dog.App;
using Dog.App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dog.Infrastructure.Repositories;

public class DogRepository(DogContext context) : IDogRepository
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

    public async Task<int> CreateDog(Domain.Dog dog)
    {
        context.Dogs.Add(dog);
        return await context.SaveChangesAsync();
    }

    public IAsyncEnumerable<Domain.Dog> GetAllDogs()
    {
        return context.Dogs.AsAsyncEnumerable(); 
    }

    public async Task<bool> UpdateDog(Domain.Dog dog)
    {
        context.Entry(dog).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}