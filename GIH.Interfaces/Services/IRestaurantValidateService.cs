namespace GIH.Interfaces.Services;

public interface IRestaurantValidateService
{
    bool ValidateRestaurant(string nickname, string password);
}