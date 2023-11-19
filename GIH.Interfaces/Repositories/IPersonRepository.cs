using GIH.Entities;

namespace GIH.Interfaces.Repositories;

public interface IPersonRepository
{
    IQueryable<Person> GetPerson();
    Person GetPersonById(int id);
    Person GetPersonByEmail(string email);
    void CreatePerson(Person person);
    void UpdatePerson(Person person);
    void UpdatePersonPassword(Person person);
    void DeletePerson(Person person);
}