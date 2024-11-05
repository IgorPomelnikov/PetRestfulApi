namespace Dog.App.Repositories;

public interface IDogRepository
{
    public Task<Domain.Dog?> GetDog(int id);
    public Task<IEnumerable<Domain.Dog>> GetDogs(DogSearchParameters parameters);
    public Task<int> CreateDog(Domain.Dog dog);
    public Task<bool> UpdateDog(Domain.Dog dog);
    
}