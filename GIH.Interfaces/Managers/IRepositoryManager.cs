using GIH.Interfaces.Repositories;

namespace GIH.Interfaces.Managers;

public interface IRepositoryManager
{
    IPersonRepository PersonRepository { get; }
    void Save();
}