using System.Security.Cryptography;
using System.Text;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;

namespace GIH.Services;

public class PersonValidateService : IPersonValidateService
{

    private readonly IRepositoryManager _repositoryManager;

    public PersonValidateService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public bool ValidatePerson(string nickname, string password)
    {
        
        var person = _repositoryManager.PersonRepository.GetPersonByNickName(nickname);

        if (person == null)
        {
            return false;
        }

        var validUsername = person.PersonNickName;
        var validPassword = person.PersonPassword;
        var validPasswordSalt = person.PasswordSalt;

        if (validUsername != null && validPassword != null && validPasswordSalt != null)
        {
            var validVerifyPassword = PasswordHasher.VerifyPassword(password, validPassword, validPasswordSalt);

            if (nickname == validUsername && validVerifyPassword)
            {
                return true;
            }
        }

        return false;
    }
}