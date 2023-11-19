using GIH.Entities;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;

namespace GIH.Interfaces.Services;

public interface IPersonService
{
    IQueryable<Person> GetPerson();
    Person GetPersonById(int id);
    void CreatePerson(Person person);
    void UpdatePersonById(int id,Person person);
    void DeletePersonById(int id);
    Person GetPersonByEmail(string email);
    bool UpdatePassword(string personEmail, string currentPassword, string newPassword);
}