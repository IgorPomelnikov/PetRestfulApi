using Contracts;

namespace Dog.App.Repositories;

public interface IDogRepository
{
    Task<Domain.Dog?> GetDog(int id);
    Task<IEnumerable<Domain.Dog>> GetDogs(DogSearchParameters parameters);
    Task<PetsToyDto?> GetPetsToy(int dogId);
    Task<PetsToyDto?> GetDogWithToy(int dogId);
    Task<int> CreateDog(Domain.Dog dog);
    Task<bool> UpdateDog(Domain.Dog dog);

    IAsyncEnumerable<Domain.Dog> GetAllDogs();
    
}