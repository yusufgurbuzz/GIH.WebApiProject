using GIH.Entities;
using GIH.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GIH.Repositories;

public class PersonRepository:RepositoryBase<Person>,IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }   
    public IQueryable<Person> GetPerson()
    {
        return FindAll();
    }

    public Person GetPersonById(int id)
    {
        return FindByCondition(b => b.PersonId.Equals(id)).SingleOrDefault();
    }
    public Person GetPersonByEmail(string email)
    {
        return FindByCondition(b => b.PersonEmail.Equals(email)).SingleOrDefault();
    }

    public void CreatePerson(Person person)
    {
        Create(person);
    }

    public void UpdatePerson(Person person)
    {
        Update(person);
    }
    
    public void UpdatePersonPassword(Person person)
    {
        Update(person);
    }

    public void DeletePerson(Person person)
    {
        Delete(person);
    }
}