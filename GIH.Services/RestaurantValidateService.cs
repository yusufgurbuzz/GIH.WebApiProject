using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;

namespace GIH.Services;

public class RestaurantValidateService : IRestaurantValidateService
{
    private readonly IRepositoryManager _repositoryManager;

    public RestaurantValidateService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public bool ValidateRestaurant(string nickname, string password)
    {
        var validUsername = _repositoryManager.RestaurantRepository.GetRestaurantByNickName(nickname).restaurantNickname;
        var validPassword = _repositoryManager.RestaurantRepository.GetRestaurantByNickName(nickname).restaurantPassword;
        var validPasswordSalt = _repositoryManager.RestaurantRepository.GetRestaurantByNickName(nickname).PasswordSalt;
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