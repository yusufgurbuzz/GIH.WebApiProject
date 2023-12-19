using GIH.Interfaces.Repositories;

namespace GIH.Interfaces.Managers;

public interface IRepositoryManager
{
    IPersonRepository PersonRepository { get; }
    IRestaurantRepository RestaurantRepository { get; }
    IAdvertRepository AdvertRepository {get;}
    void Save();
}