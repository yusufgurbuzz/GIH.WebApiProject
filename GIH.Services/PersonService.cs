using System.Security.Cryptography;
using System.Text;
using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;

namespace GIH.Services;

public class PersonService : IPersonService
{
    private readonly IRepositoryManager _repositoryManager;
  
    public PersonService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    
    public IQueryable<Person> GetPerson()
    {
        return _repositoryManager.PersonRepository.GetPerson();
    }

    public Person GetPersonById(int id)
    {
        return _repositoryManager.PersonRepository.GetPersonById(id);
    }
    
    public Person GetPersonByEmail(string email)
    {
        return _repositoryManager.PersonRepository.GetPersonByEmail(email);
    }

    public Person GetPersonByNickName(string nickName)
    {
        return _repositoryManager.PersonRepository.GetPersonByNickName(nickName);
    }

    public bool UpdatePassword(string email, string currentPassword, string newPassword)
    {
        var person = _repositoryManager.PersonRepository.GetPersonByEmail(email);
        if (person is null)
        {
            throw new InvalidOperationException("Böyle bir kullanıcı yok");
        }

        if (!PasswordHasher.VerifyPassword(currentPassword, person.PersonPassword,person.PasswordSalt))
        {
            throw new InvalidOperationException("Şifre doğrulanamadı");
            
        }
         // Yeni şifreyi şifrele ve güncelle
         var (newHashedPassword, newSalt) = PasswordHasher.HashPassword(newPassword);
         person.PersonPassword = newHashedPassword;
         person.PasswordSalt = newSalt;
         
         _repositoryManager.PersonRepository.UpdatePersonPassword(person);
         _repositoryManager.Save();
         return true;
    }

    public void CreatePerson(Person person)
    {
        var personEmail = _repositoryManager.PersonRepository.GetPersonByEmail(person.PersonEmail);
        var personNickName = _repositoryManager.PersonRepository.GetPersonByNickName(person.PersonNickName);
        if ( (personEmail is not null)|| personNickName is not null)
        {
            throw new InvalidOperationException("Bu mail adresi vey kullanıcı adı kullanılmaktadır.");
        }
        
        var (hashedPassword, salt) = PasswordHasher.HashPassword(person.PersonPassword);
        
        var newPerson = new Person
        {
            PersonName = person.PersonName, 
            PersonSurname = person.PersonSurname,
            PersonEmail = person.PersonEmail,
            PersonPassword = hashedPassword,
            PersonPhoneNumber = person.PersonPhoneNumber,
            PersonNickName = person.PersonNickName,
            RoleId = person.RoleId,
            PasswordSalt = salt
        };
        
        _repositoryManager.PersonRepository.CreatePerson(newPerson);
        _repositoryManager.Save();
    }

    public void UpdatePersonById(int id, PersonDto person)
    {
        var entity = _repositoryManager.PersonRepository.GetPersonById(id);
        
        if (entity is null)
        {
            throw new Exception($"Person with id : {id} could not found");
        }

        if (person is null)
        {
            throw new Exception($"Person is null");
        }

        entity.PersonId = person.PersonId;
        entity.PersonName = person.PersonName;
        entity.PersonSurname = person.PersonSurname;
        entity.PersonPhoneNumber = person.PersonPhoneNumber;
        
        _repositoryManager.PersonRepository.UpdatePerson(entity);
        _repositoryManager.Save();
    }

    public void DeletePersonById(int id)
    {
        var entity = _repositoryManager.PersonRepository.GetPersonById(id);
        if (entity is null)
        {
            throw new Exception($"Person with id : {id} could not found");
        }
        
        _repositoryManager.PersonRepository.DeletePerson(entity);
        _repositoryManager.Save();
    }
}