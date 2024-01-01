using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;
using GIH.Services;
using Moq;
using Xunit;
namespace GIH.Test;

public class AdvertServiceTest
{
    [Fact]
    public void GetAdvert_ShouldReturnAdvertQueryable()
    {
        // Arrange
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        var expectedAdverts = new List<Advert>
        {
            new Advert { AdvertId = 1, AdvertDescription = "ilan açıklama", AdvertKilo = 2,AdvertName = "ilan",RestaurantId = 2},
            new Advert { AdvertId = 1, AdvertDescription = "ilan açıklama", AdvertKilo = 2,AdvertName = "ilan",RestaurantId = 3}
        };
        
        mockAdvertRepository.Setup(repo => repo.GetAdvert()).Returns(expectedAdverts.AsQueryable());
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var result = advertService.GetAdvert();
        
        Assert.NotNull(result);
    }
    [Fact]
    public void GetAdvertById_ShouldReturnAdvert_WhenAdvertExists()
    {
        int advertId = 1;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        var expectedAdvert = new Advert
        {
            AdvertId = advertId, AdvertDescription = "ilan açıklama", AdvertKilo = 2, AdvertName = "ilan", RestaurantId = 2
        };
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertById(advertId)).Returns(expectedAdvert);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var result = advertService.GetAdvertById(advertId);
        
        Assert.NotNull(result);
    }

    [Fact]
    public void GetAdvertById_ShouldReturnNull_WhenAdvertDoesNotExist()
    {
        int advertId = 2;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertById(advertId)).Returns((Advert)null);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var result = advertService.GetAdvertById(advertId);
        
        Assert.Null(result);
    }
    [Fact]
    public void GetAdvertByAddress_ShouldReturnAdverts_WhenAddressExists()
    {
        string address = "şişli";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        var expectedAdverts = new List<Advert>
        {
            new Advert { AdvertId = 1, AdvertDescription = "ilan açıklama", AdvertKilo = 2,AdvertName = "ilan",RestaurantId = 2},
            new Advert { AdvertId = 2, AdvertDescription = "ilan açıklama", AdvertKilo = 2,AdvertName = "ilan",RestaurantId = 3}
        };
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertByAdress(address)).Returns(expectedAdverts);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var result = advertService.GetAdvertByAdress(address);
        
        Assert.NotNull(result);
    }

    [Fact]
    public void GetAdvertByAddress_ShouldReturnEmptyList_WhenAddressDoesNotExist()
    {
        string address = "abc";
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertByAdress(address)).Returns(new List<Advert>());
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var result = advertService.GetAdvertByAdress(address);
        
        Assert.Empty(result);
    }
    [Fact]
    public void GetAdvertByRestaurantId_ShouldReturnAdverts_WhenRestaurantIdExists()
    {
        int restaurantId = 1;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        var expectedAdverts = new List<Advert>
        {
            new Advert { AdvertId = 1, AdvertDescription = "ilan açıklama", AdvertKilo = 2,AdvertName = "ilan",RestaurantId = restaurantId},
            new Advert { AdvertId = 2, AdvertDescription = "ilan açıklama", AdvertKilo = 2,AdvertName = "ilan",RestaurantId = restaurantId}
        };
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertByRestaurantId(restaurantId)).Returns(expectedAdverts);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var result = advertService.GetAdvertByRestaurantId(restaurantId);
        
        Assert.Equal(expectedAdverts, result.ToList()); 
    }

    [Fact]
    public void GetAdvertByRestaurantId_ShouldReturnEmptyList_WhenRestaurantIdDoesNotExist()
    {
        int restaurantId = 2; //var olmayan restaurantId
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertByRestaurantId(restaurantId)).Returns(new List<Advert>());
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var result = advertService.GetAdvertByRestaurantId(restaurantId);
        
        Assert.Empty(result);
    }
    [Fact]
    public void CreateAdvert_ShouldThrowException_WhenAdvertIsNull()
    {
        Advert nullAdvert = null;
        var mockRepository = new Mock<IRepositoryManager>();
        var advertService = new AdvertService(mockRepository.Object);
        
        Assert.Throws<NullReferenceException>(() => advertService.CreateAdvert(nullAdvert));
    }

    [Fact]
    public void CreateAdvert_ShouldCreateAdvert_WhenAdvertIsNotNull()
    {
        var advert = new Advert
        {
             AdvertId = 1, AdvertDescription = "ilan açıklama", AdvertKilo = 2,AdvertName = "ilan",RestaurantId = 2
           
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        advertService.CreateAdvert(advert);
        
        mockAdvertRepository.Verify(repo => repo.CreateAdvert(advert));
        mockRepository.Verify(manager => manager.Save());
    }
    
    [Fact]
    public void UpdateAdvertById_ShouldThrowException_WhenAdvertNotFound()
    {
        int advertId = 1;
        var advertDto = new AdvertDto
        {
            AdvertName = "İlan adı",
            AdvertDescription = "açıklama",
            AdvertKilo = 10
            
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertById(advertId)).Returns((Advert)null);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var exception = Assert.Throws<Exception>(() => advertService.UpdateAdvertById(advertId, advertDto));
        Assert.Equal($"Advert with id : {advertId} could not found", exception.Message);
    }

    [Fact]
    public void UpdateAdvertById_ShouldThrowException_WhenAdvertDtoIsNull()
    {
        int advertId = 1;
        AdvertDto nullAdvertDto = null;

        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertById(advertId)).Returns(new Advert());
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var exception = Assert.Throws<Exception>(() => advertService.UpdateAdvertById(advertId, nullAdvertDto));
        Assert.Equal("Advert is null", exception.Message);
    }

    [Fact]
    public void UpdateAdvertById_ShouldUpdateAdvert_WhenAdvertFoundAndDtoIsNotNull()
    {
        int advertId = 1;
        var advertDto = new AdvertDto
        {
            AdvertName = "güncel ad",
            AdvertDescription = "güncel açıklama",
            AdvertKilo = 15
        };

        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        var mevcutilan = new Advert
        {
            AdvertId = advertId
        };
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertById(advertId)).Returns(mevcutilan);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        advertService.UpdateAdvertById(advertId, advertDto);
        
        mockAdvertRepository.Verify(repo => repo.UpdateAdvert(mevcutilan));
        mockRepository.Verify(manager => manager.Save());
    }
    [Fact]
    public void DeleteAdvertById_ShouldThrowException_WhenAdvertNotFound()
    {
        int advertId = 1;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertById(advertId)).Returns((Advert)null);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        var exception = Assert.Throws<Exception>(() => advertService.DeleteAdvertById(advertId));
        Assert.Equal($"Advert with id : {advertId} could not found", exception.Message);
    }

    [Fact]
    public void DeleteAdvertById_ShouldDeleteAdvert_WhenAdvertFound()
    {
        int advertId = 1;
        var mockRepository = new Mock<IRepositoryManager>();
        var mockAdvertRepository = new Mock<IAdvertRepository>();
        var existingAdvert = new Advert
        {
            AdvertId = advertId
        };
        
        mockAdvertRepository.Setup(repo => repo.GetAdvertById(advertId)).Returns(existingAdvert);
        mockRepository.Setup(manager => manager.AdvertRepository).Returns(mockAdvertRepository.Object);

        var advertService = new AdvertService(mockRepository.Object);
        
        advertService.DeleteAdvertById(advertId);
        
        mockAdvertRepository.Verify(repo => repo.DeleteAdvert(existingAdvert));
        mockRepository.Verify(manager => manager.Save());
    }
}