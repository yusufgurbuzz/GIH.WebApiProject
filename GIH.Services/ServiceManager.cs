using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;

namespace GIH.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IPersonService> _personService;
    private readonly Lazy<IRestaurantService> _restaurantService;
    private readonly Lazy <IAdvertService> _advertService;
    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _personService = new Lazy<IPersonService>(() => new PersonService(repositoryManager));
        _restaurantService = new Lazy<IRestaurantService>(() => new RestaurantService(repositoryManager));
        _advertService = new Lazy<IAdvertService>(()=> new AdvertService(repositoryManager));
    }

    public IPersonService PersonService => _personService.Value;
    public IRestaurantService RestaurantService => _restaurantService.Value;

    public IAdvertService AdvertService => _advertService.Value;
}