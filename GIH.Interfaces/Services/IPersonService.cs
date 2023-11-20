using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;

namespace GIH.Interfaces.Services;

public interface IPersonService
{
    IQueryable<Person> GetPerson();
    Person GetPersonById(int id);
    void CreatePerson(Person person);
    void UpdatePersonById(int id,PersonDto person);
    void DeletePersonById(int id);
    Person GetPersonByEmail(string email);
    Person GetPersonByNickName(string nickName);
    bool UpdatePassword(string personEmail, string currentPassword, string newPassword);
}