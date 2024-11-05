namespace Dog.App.Repositories;

public interface IDogRepository
{
    Task<Domain.Dog?> GetDog(int id);
    Task<IEnumerable<Domain.Dog>> GetDogs(DogSearchParameters parameters);
    Task<int> CreateDog(Domain.Dog dog);
    Task<bool> UpdateDog(Domain.Dog dog);

    IAsyncEnumerable<Domain.Dog> GetAllDogs();
}