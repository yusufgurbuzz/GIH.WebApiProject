using GIH.Entities;

namespace GIH.Interfaces.Repositories;

public interface IRestaurantRepository
{
    IQueryable<Restaurant> GetRestaurant();
    Restaurant GetRestaurantById(int id);
    Restaurant GetRestaurantByEmail(string email);
    IQueryable<Restaurant> GetRestaurantByAdress(string adress);
    Restaurant GetRestaurantByNickName(string nickname);
    void CreateRestaurant(Restaurant restaurant);
    void UpdateRestaurant(Restaurant restaurant);
    void UpdateRestaurantPassword(Restaurant restaurant);
    void DeleteRestaurant(Restaurant restaurant);
}