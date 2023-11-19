using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;

namespace GIH.Repositories;

public class RepositoryManager:IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private Lazy<IPersonRepository> _personRepository;

    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        _personRepository = new Lazy<IPersonRepository>(()=> new PersonRepository(_context));
    }

    public IPersonRepository PersonRepository => _personRepository.Value;
    
    public void Save()
    {
        _context.SaveChanges();
    }
}