using GIH.Interfaces.Services;

namespace GIH.Interfaces.Managers;

public interface IServiceManager
{
    IPersonService PersonService { get; }
}