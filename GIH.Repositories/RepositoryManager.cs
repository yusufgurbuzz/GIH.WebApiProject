using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;

namespace GIH.Repositories;

public class RepositoryManager:IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private Lazy<IPersonRepository> _personRepository;
    private Lazy<IRestaurantRepository> _restaurantRepository;
    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        _personRepository = new Lazy<IPersonRepository>(()=> new PersonRepository(_context));
        _restaurantRepository = new Lazy<IRestaurantRepository>(()=> new RestaurantRepository(_context));
    }

    public IPersonRepository PersonRepository => _personRepository.Value;
    public IRestaurantRepository RestaurantRepository => _restaurantRepository.Value;

    public void Save()
    {
        _context.SaveChanges();
    }
}