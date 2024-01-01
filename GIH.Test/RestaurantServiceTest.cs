using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;
using GIH.Services;
using Moq;

namespace GIH.Test;

public class RestaurantServiceTest
{
    [Fact]
    public void GetRestaurant_ShouldReturnRestaurants()
    {
        // Arrange
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        var expectedRestaurants = new List<Restaurant>
        {
            new Restaurant
            {
                restaurantId = 1, restaurantName = "Restaurant 1",restaurantMail = "abc@gmail.com",restaurantNickname = "abc",
                PasswordSalt = "",restaurantPassword = "233123avb",restaurantNumber = "11111111",restaurantAdress = "şişli",RoleId = 2
            },
            new Restaurant
            {
                restaurantId = 2, restaurantName = "Restaurant 2",restaurantMail = "abcdegf@gmail.com",restaurantNickname = "abc3er",
                PasswordSalt = "",restaurantPassword = "2adas3avb",restaurantNumber = "11324231",restaurantAdress = "kadıköy",RoleId = 2
            },
        };
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurant()).Returns(expectedRestaurants.AsQueryable());
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurant();
        
        Assert.NotNull(result);
    }

    [Fact]
    public void GetRestaurantById_ShouldReturnRestaurant_WhenRestaurantExists()
    {
        int restaurantId = 1;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        var expectedRestaurant = new Restaurant
        {
            restaurantId = restaurantId, restaurantName = "Restaurant 1", restaurantMail = "abc@gmail.com",
            restaurantNickname = "abc",
            PasswordSalt = "", restaurantPassword = "233123avb", restaurantNumber = "11111111",
            restaurantAdress = "şişli", RoleId = 2
        };
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantById(restaurantId)).Returns(expectedRestaurant);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurantById(restaurantId);
        
        Assert.Equal(expectedRestaurant, result);
    }

    [Fact]
    public void GetRestaurantById_ShouldReturnNull_WhenRestaurantDoesNotExist()
    {
        int restaurantId = 2; // var olmayan restaurantId
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantById(restaurantId)).Returns((Restaurant)null);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurantById(restaurantId);
        
        Assert.Null(result);
    }

    [Fact]
    public void GetRestaurantByEmail_ShouldReturnRestaurant_WhenRestaurantExists()
    {
        string email = "gih@gmail.com";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        var expectedRestaurant = new Restaurant  {
            restaurantId = 1, restaurantName = "Restaurant 1", restaurantMail = email,
            restaurantNickname = "abc",
            PasswordSalt = "", restaurantPassword = "233123avb", restaurantNumber = "11111111",
            restaurantAdress = "şişli", RoleId = 2
        };
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByEmail(email)).Returns(expectedRestaurant);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurantByEmail(email);
        
        Assert.Equal(expectedRestaurant, result);
    }

    [Fact]
    public void GetRestaurantByEmail_ShouldReturnNull_WhenRestaurantDoesNotExist()
    {
        string email = "varolmayanemail@gmail.com";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByEmail(email)).Returns((Restaurant)null);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurantByEmail(email);
        
        Assert.Null(result);
    }

    [Fact]
    public void GetRestaurantByAdress_ShouldReturnRestaurants()
    {
        string address = "ayazağa";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        var eslesenRestaurant = new List<Restaurant>
        {
            new Restaurant
            {
                restaurantId = 1, restaurantName = "Restaurant 1",restaurantMail = "abc@gmail.com",restaurantNickname = "abc",
                PasswordSalt = "",restaurantPassword = "233123avb",restaurantNumber = "11111111",restaurantAdress = address,RoleId = 2
            },
            new Restaurant
            {
                restaurantId = 2, restaurantName = "Restaurant 2",restaurantMail = "abcdegf@gmail.com",restaurantNickname = "abc3er",
                PasswordSalt = "",restaurantPassword = "2adas3avb",restaurantNumber = "11324231",restaurantAdress = address,RoleId = 2
            },
        };
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByAdress(address)).Returns(eslesenRestaurant.AsQueryable());
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurantByAdress(address);
        
        Assert.Equal(eslesenRestaurant, result.ToList());
    }

    [Fact]
    public void GetRestaurantByNickName_ShouldReturnRestaurant_WhenRestaurantExists()
    {
        string nickName = "gih";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        var eslesenRestaurant = new Restaurant
        {
            restaurantId = 2, restaurantName = "Restaurant 2", restaurantMail = "abcdegf@gmail.com",
            restaurantNickname = nickName,
            PasswordSalt = "", restaurantPassword = "2adas3avb", restaurantNumber = "11324231",
            restaurantAdress = "address", RoleId = 2
        };
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByNickName(nickName)).Returns(eslesenRestaurant);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurantByNickName(nickName);
        
        Assert.Equal(eslesenRestaurant, result);
    }

    [Fact]
    public void GetRestaurantByNickName_ShouldReturnNull_WhenRestaurantDoesNotExist()
    {
        string nickName = "varOlmayanKullanıcıAdı";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByNickName(nickName)).Returns((Restaurant)null);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var result = restaurantService.GetRestaurantByNickName(nickName);
        
        Assert.Null(result);
    }
    [Fact]
    public void CreateRestaurant_ShouldThrowException_WhenEmailOrNickNameAlreadyUsed()
    {
         var restaurant = new Restaurant
                {
                    restaurantName = "Restaurant",
                    restaurantAdress = "Address",
                    restaurantMail = "testmaili@gmail.com",
                    restaurantNickname = "Nick", 
                    restaurantNumber = "1234567890",
                    restaurantPassword = "abcd",
                    RoleId = 2
                };
         
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByEmail(restaurant.restaurantMail)).Returns(new Restaurant());
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByNickName(restaurant.restaurantNickname)).Returns(new Restaurant());
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var exception = Assert.Throws<InvalidOperationException>(() => restaurantService.CreateRestaurant(restaurant));
        Assert.Equal("This e-mail address or username is used.", exception.Message);
    }

    [Fact]
    public void CreateRestaurant_ShouldCreateRestaurant_WhenEmailAndNickNameAreUnique()
    { 
        var restaurant = new Restaurant
             {
                 restaurantId = 11,
                 restaurantName = "Restaurant",
                 restaurantAdress = "Address",
                 restaurantMail = "testmaili@gmail.com",
                 restaurantNickname = "Nick", 
                 restaurantNumber = "1234567890",
                 restaurantPassword = "abcd",
                 RoleId = 2
             };
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByEmail(restaurant.restaurantMail)).Returns((Restaurant)null);
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantByNickName(restaurant.restaurantNickname)).Returns((Restaurant)null);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        restaurantService.CreateRestaurant(restaurant);
        
        mockRestaurantRepository.Verify(repo => repo.CreateRestaurant(It.IsAny<Restaurant>()));
        mockRepository.Verify(manager => manager.Save());
    }
    [Fact]
    public void UpdateRestaurantById_ShouldThrowException_WhenRestaurantNotFound()
    {
        int restaurantId = 17; //var olmayan restaurantId
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantById(restaurantId)).Returns((Restaurant)null);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var exception = Assert.Throws<Exception>(() => restaurantService.UpdateRestaurantById(restaurantId, new RestaurantDto()));
        Assert.Equal($"Restaurant with id : {restaurantId} could not found", exception.Message);
    }

    [Fact]
    public void UpdateRestaurantById_ShouldUpdateRestaurant_WhenRestaurantFound()
    {
        int restaurantId = 155; // var olan restaurantId
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        var existingRestaurant = new Restaurant
        {
            restaurantId = restaurantId, restaurantName = "eski Restaurant",restaurantNumber = "111",restaurantAdress = "şişli"
            
        };
        var updatedRestaurantDto = new RestaurantDto
        {
            restaurantId = restaurantId, restaurantName = "yeni Restaurant",restaurantPhoneNumber = "111",restaurantAdress = "şişli"
            
        };
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantById(restaurantId)).Returns(existingRestaurant);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        restaurantService.UpdateRestaurantById(restaurantId, updatedRestaurantDto);
        
        mockRestaurantRepository.Verify(repo => repo.UpdateRestaurant(existingRestaurant));
        mockRepository.Verify(manager => manager.Save());
    }
    [Fact]
    public void DeleteRestaurantById_ShouldThrowException_WhenRestaurantNotFound()
    {
        
        int restaurantId = 10;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantById(restaurantId)).Returns((Restaurant)null);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        var exception = Assert.Throws<Exception>(() => restaurantService.DeleteRestaurantById(restaurantId));
        Assert.Equal($"Restaurant with id : {restaurantId} could not found", exception.Message);
    }

    [Fact]
    public void DeleteRestaurantById_ShouldDeleteRestaurant_WhenRestaurantFound()
    {
        int restaurantId = 22;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockRestaurantRepository = new Mock<IRestaurantRepository>();
        var existingRestaurant = new Restaurant { restaurantId = restaurantId, restaurantName = "Restaurant" };
        
        mockRestaurantRepository.Setup(repo => repo.GetRestaurantById(restaurantId)).Returns(existingRestaurant);
        mockRepository.Setup(manager => manager.RestaurantRepository).Returns(mockRestaurantRepository.Object);

        var restaurantService = new RestaurantService(mockRepository.Object);
        
        restaurantService.DeleteRestaurantById(restaurantId);
        
        mockRestaurantRepository.Verify(repo => repo.DeleteRestaurant(existingRestaurant));
        mockRepository.Verify(manager => manager.Save());
    }
}