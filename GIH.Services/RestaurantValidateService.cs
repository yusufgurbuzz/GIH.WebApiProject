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
        var restaurant = _repositoryManager.RestaurantRepository.GetRestaurantByNickName(nickname);
        
        if (restaurant == null)
        {
            return false;
        }

        var validUsername = restaurant.restaurantNickname;
        var validPassword = restaurant.restaurantPassword;
        var validPasswordSalt = restaurant.PasswordSalt;
        
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