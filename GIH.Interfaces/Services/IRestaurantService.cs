using GIH.Entities;
using GIH.Entities.DTOs;

namespace GIH.Interfaces.Services;

public interface IRestaurantService
{
    IQueryable<Restaurant> GetRestaurant();
    Restaurant GetRestaurantById(int id);
    void CreateRestaurant(Restaurant restaurant);
    void UpdateRestaurantById(int id, RestaurantDto restaurantDto);
    public IQueryable<Restaurant> GetRestaurantByAdress(string adress);
    void DeleteRestaurantById(int id);
    Restaurant GetRestaurantByEmail(string email);
    Restaurant GetRestaurantByNickName(string nickName);
    bool UpdatePassword(string personEmail, string currentPassword, string newPassword);
}