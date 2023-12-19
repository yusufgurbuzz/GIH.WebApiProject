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
       
        var validUsername = _repositoryManager.PersonRepository.GetPersonByNickName(nickname).PersonNickName;
        var validPassword = _repositoryManager.PersonRepository.GetPersonByNickName(nickname).PersonPassword;
        var validPasswordSalt = _repositoryManager.PersonRepository.GetPersonByNickName(nickname).PasswordSalt;
        var validVerifyPassword = PasswordHasher.VerifyPassword(password, validPassword, validPasswordSalt);

        if (nickname == validUsername && validVerifyPassword is true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}