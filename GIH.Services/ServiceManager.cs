using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;

namespace GIH.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IPersonService> _personService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _personService = new Lazy<IPersonService>(() => new PersonService(repositoryManager));
    }

    public IPersonService PersonService => _personService.Value;
}