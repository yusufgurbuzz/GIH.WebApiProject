using GIH.Entities;
using GIH.Interfaces.Managers;
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
}