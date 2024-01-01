using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;
using GIH.Services;
using Moq;

namespace GIH.Test;

public class PersonServiceTest
{
    [Fact]
    public void GetPerson_WhenCalled_ShouldReturnPerson()
    {
        var person = new List<Person>
        {
            new Person
            {
                PersonId = 1,PersonName = "Test",PersonSurname = "Test2",PersonEmail = "abc@gmail.com",PersonNickName = "fury",
                PersonPassword = "abc123",PersonPhoneNumber = "5363298411",RoleId = 1
            },
            new Person
            {
                PersonId = 2,PersonName = "Test",PersonSurname = "Test2",PersonEmail = "abcdef@gmail.com",PersonNickName = "hurry",
                PersonPassword = "abc123",PersonPhoneNumber = "5363298411",RoleId = 1
            }
        };

        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var personService = new PersonService(mockRepositoryManager.Object);
        mockRepositoryManager.Setup(repo=>repo.PersonRepository.GetPerson())
            .Returns(person.AsQueryable());

        var result = personService.GetPerson().ToList();
        Assert.Equal(person.Count,result.Count);
    }

    [Fact]
    public void GetPersonById_WhenValidIdProvided_ShouldReturnPerson()
    {
        var person = new Person
        {
            PersonId = 2, PersonName = "Test", PersonSurname = "Test2", PersonEmail = "abcdef@gmail.com",
            PersonNickName = "hurry", PersonPassword = "abc123", PersonPhoneNumber = "5363298411", RoleId = 1
        };

        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var personService = new PersonService(mockRepositoryManager.Object);

        mockRepositoryManager.Setup(repo=>repo.PersonRepository.GetPersonById(It.IsAny<int>()))
            .Returns(person);
        
        var result = personService.GetPersonById(2);
        Assert.NotNull(result);
    }
    [Fact]
    public void GetPersonById_WhenInvalidIdProvided_ShouldReturnPerson()
    {
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var personService = new PersonService(mockRepositoryManager.Object);

        mockRepositoryManager.Setup(repo=>repo.PersonRepository.GetPersonById(It.IsAny<int>()))
            .Returns((Person)null);
        
        var result = personService.GetPersonById(999);
        Assert.Null(result);
    }
    
    [Fact]
    public void GetPersonByNickName_ShouldReturnPerson_WhenNickNameExists()
    {
        string nickname = "yusufg";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();
        var expectedPerson = new Person
        {
            PersonId = 1,PersonName = "yusuf",PersonSurname = "gürbüz",PersonEmail = "abc@gmail.com",
            PersonPassword = "",PersonNickName = nickname,PersonPhoneNumber = "1111111111",RoleId = 1,PasswordSalt = ""
        };

        mockPersonRepository.Setup(repo => repo.GetPersonByNickName(nickname)).Returns(expectedPerson);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        var result = personService.GetPersonByNickName(nickname);
        
        Assert.NotNull(result);
        Assert.Equal(expectedPerson, result);
    }

    [Fact]
    public void GetPersonByNickName_ShouldReturnNull_WhenNickNameDoesNotExist()
    {
        string nickName = "bilinmeyenNickname";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();

        mockPersonRepository.Setup(repo => repo.GetPersonByNickName(nickName)).Returns((Person)null);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        var result = personService.GetPersonByNickName(nickName);
        
        Assert.Null(result);
    }
    
    [Fact]
    public void UpdatePassword_ShouldThrowException_WhenPersonDoesNotExist()
    {
        string email = "yusufgurbuz@gmail.com";
        string eskiSifre = "eskiSifre";
        string yeniSifre = "yeniSifre";

        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();

        mockPersonRepository.Setup(repo => repo.GetPersonByEmail(email)).Returns((Person)null);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        Assert.Throws<InvalidOperationException>(() => personService.UpdatePassword(email, eskiSifre, yeniSifre));
    }
    
     [Fact]
    public void CreatePerson_ShouldThrowException_WhenEmailOrNickNameIsAlreadyUsed()
    {
        var person = new Person
        {
            PersonName = "yusuf",
            PersonSurname = "gürbüz",
            PersonEmail = "yusufgurbuz@turk.net",
            PersonPassword = "abc",
            PersonPhoneNumber = "11111111111",
            PersonNickName = "yusufgurbuz",
            RoleId = 1
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();

        mockPersonRepository.Setup(repo => repo.GetPersonByEmail(person.PersonEmail)).Returns(new Person());
        mockPersonRepository.Setup(repo => repo.GetPersonByNickName(person.PersonNickName)).Returns(new Person());
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);

        Assert.Throws<InvalidOperationException>(() => personService.CreatePerson(person));
    }

    [Fact]
    public void CreatePerson_ShouldCreatePerson_WhenEmailAndNickNameAreUnique()
    {
        
        var person = new Person
        {
            PersonName = "yusuf",
            PersonSurname = "gürbüz",
            PersonEmail = "yusufgurbuz@turk.net",
            PersonPassword = "abc",
            PersonPhoneNumber = "11111111111",
            PersonNickName = "yusufgurbuz",
            RoleId = 1
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();
        
        mockPersonRepository.Setup(repo => repo.GetPersonByEmail(person.PersonEmail)).Returns((Person)null);
        mockPersonRepository.Setup(repo => repo.GetPersonByNickName(person.PersonNickName)).Returns((Person)null);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        personService.CreatePerson(person);
        
        mockPersonRepository.Verify(repo => repo.CreatePerson(It.IsAny<Person>()), Times.Once);
    }
    [Fact]
    public void UpdatePersonById_ShouldThrowException_WhenPersonNotFound()
    {
       
        int personId = 1;
        var personDto = new PersonDto
        {
            PersonId = personId,
            PersonName = "yusuf",
            PersonSurname = "gürbüz",
            PersonPhoneNumber = "123456789"
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();
        
        mockPersonRepository.Setup(repo => repo.GetPersonById(personId)).Returns((Person)null);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        var exception = Assert.Throws<Exception>(() => personService.UpdatePersonById(personId, personDto));
        Assert.Equal($"Person with id : {personId} could not found", exception.Message);
    }

    [Fact]
    public void UpdatePersonById_ShouldUpdatePerson_WhenPersonFound()
    {
        int personId = 1;
        var personDto = new PersonDto
        {
            PersonId = personId,
            PersonName = "yusuf",
            PersonSurname = "gürbüz",
            PersonPhoneNumber = "123456789"
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();
        var mevcutKisi = new Person { PersonId = personId };

        mockPersonRepository.Setup(repo => repo.GetPersonById(personId)).Returns(mevcutKisi);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        personService.UpdatePersonById(personId, personDto);
        
        mockPersonRepository.Verify(repo => repo.UpdatePerson(mevcutKisi), Times.Once);
        mockRepository.Verify(manager => manager.Save(), Times.Once);
    }
    [Fact]
    public void DeletePersonById_ShouldThrowException_WhenPersonNotFound()
    {
        int personId = 1;

        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();
        
        mockPersonRepository.Setup(repo => repo.GetPersonById(personId)).Returns((Person)null);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        var exception = Assert.Throws<Exception>(() => personService.DeletePersonById(personId));
        Assert.Equal($"Person with id : {personId} could not found", exception.Message);
    }

    [Fact]
    public void DeletePersonById_ShouldDeletePerson_WhenPersonFound()
    {
        int personId = 1;
        var mevcutKisi = new Person
        {
            PersonId = personId
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockPersonRepository = new Mock<IPersonRepository>();
        
        mockPersonRepository.Setup(repo => repo.GetPersonById(personId)).Returns(mevcutKisi);
        mockRepository.Setup(manager => manager.PersonRepository).Returns(mockPersonRepository.Object);

        var personService = new PersonService(mockRepository.Object);
        
        personService.DeletePersonById(personId);
        
        mockPersonRepository.Verify(repo => repo.DeletePerson(mevcutKisi));
        mockRepository.Verify(manager => manager.Save());
    }
    
}