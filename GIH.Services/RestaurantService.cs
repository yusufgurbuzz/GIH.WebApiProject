using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;

namespace GIH.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRepositoryManager _repositoryManager;
  
    public RestaurantService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    
    public IQueryable<Restaurant> GetRestaurant()
    {
        return _repositoryManager.RestaurantRepository.GetRestaurant();
    }

    public Restaurant GetRestaurantById(int id)
    {
        return _repositoryManager.RestaurantRepository.GetRestaurantById(id);
    }
    
    public Restaurant GetRestaurantByEmail(string email)
    {
        return _repositoryManager.RestaurantRepository.GetRestaurantByEmail(email);
    }
    
    public IQueryable<Restaurant> GetRestaurantByAdress(string adress)
    {
        return _repositoryManager.RestaurantRepository.GetRestaurantByAdress(adress);
    }

    public Restaurant GetRestaurantByNickName(string nickName)
    {
        return _repositoryManager.RestaurantRepository.GetRestaurantByNickName(nickName);
    }
    public void CreateRestaurant(Restaurant restaurant)
    {
        var restaurantEmail = _repositoryManager.RestaurantRepository.GetRestaurantByEmail(restaurant.restaurantMail);
        var restaurantNickname = _repositoryManager.RestaurantRepository.GetRestaurantByNickName(restaurant.restaurantNickname);
        if ( restaurantEmail is not null || restaurantNickname is not null)
        {
            throw new InvalidOperationException("Bu mail adresi vey kullanıcı adı kullanılmaktadır.");
        }
        
        var (hashedPassword, salt) = PasswordHasher.HashPassword(restaurant.restaurantPassword);

        var newRestaurant = new Restaurant
        {
            restaurantName = restaurant.restaurantName,
            restaurantAdress = restaurant.restaurantAdress,
            restaurantMail = restaurant.restaurantMail,
            restaurantNickname = restaurant.restaurantNickname,
            restaurantNumber = restaurant.restaurantNumber,
            restaurantPassword = hashedPassword,
            PasswordSalt = salt,
            RoleId = restaurant.RoleId
        };
        
        _repositoryManager.RestaurantRepository.CreateRestaurant(newRestaurant);
        _repositoryManager.Save();
    }

    public void UpdateRestaurantById(int id, RestaurantDto restaurantDto)
    {
        var entity = _repositoryManager.RestaurantRepository.GetRestaurantById(id);
        if (entity is null)
        {
            throw new Exception($"Restaurant with id : {id} could not found");
        }
        if (restaurantDto is null)
        {
            throw new Exception($"Restaurant is null");
        }

        entity.restaurantId = restaurantDto.restaurantId;
        entity.restaurantName = restaurantDto.restaurantName;
        entity.restaurantNumber = restaurantDto.restaurantPhoneNumber;
        entity.restaurantAdress = restaurantDto.restaurantAdress;

        _repositoryManager.RestaurantRepository.UpdateRestaurant(entity);
        _repositoryManager.Save();
    }

    public void DeleteRestaurantById(int id)
    {
        var entity = _repositoryManager.RestaurantRepository.GetRestaurantById(id);
        if (entity is null)
        {
            throw new Exception($"Restaurant with id : {id} could not found");
        }
        _repositoryManager.RestaurantRepository.DeleteRestaurant(entity);
        _repositoryManager.Save();
    }
    
    public bool UpdatePassword(string email, string currentPassword, string newPassword)
    {
        var restaurant = _repositoryManager.RestaurantRepository.GetRestaurantByEmail(email);
        if (restaurant is null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (!PasswordHasher.VerifyPassword(currentPassword, restaurant.restaurantPassword,restaurant.PasswordSalt))
        {
            throw new InvalidOperationException("Password could not be verified");
            
        }
        //Sha 256 ya göre yeni şifreyi şifreleyerek restorantı update et.
        var (newHashedPassword, newSalt) = PasswordHasher.HashPassword(newPassword);
        restaurant.restaurantPassword = newHashedPassword;
        restaurant.PasswordSalt = newSalt;
         
        _repositoryManager.RestaurantRepository.UpdateRestaurantPassword(restaurant);
        _repositoryManager.Save();
        return true;
    }
}