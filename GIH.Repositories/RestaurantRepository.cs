using GIH.Entities;
using GIH.Interfaces.Repositories;

namespace GIH.Repositories;

public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Restaurant> GetRestaurant()
    {
        return FindAll();
    }

    public Restaurant GetRestaurantById(int id)
    {
        return FindByCondition(b => b.restaurantId.Equals(id)).SingleOrDefault();
    }

    public Restaurant GetRestaurantByEmail(string email)
    {
        return FindByCondition(b => b.restaurantMail.Equals(email)).SingleOrDefault();
    }

    public IQueryable<Restaurant> GetRestaurantByAdress(string adress)
    {
        return  FindByCondition(b => b.restaurantAdress.Equals(adress));
    }

    public Restaurant GetRestaurantByNickName(string nickname)
    {
        return FindByCondition(b => b.restaurantNickname.Equals(nickname)).SingleOrDefault();
    }

    public void CreateRestaurant(Restaurant restaurant)
    {
        Create(restaurant);
    }

    public void UpdateRestaurant(Restaurant restaurant)
    {
        Update(restaurant);
    }

    public void UpdateRestaurantPassword(Restaurant restaurant)
    {
        Update(restaurant);
    }

    public void DeleteRestaurant(Restaurant restaurant)
    {
        Delete(restaurant);
    }
}