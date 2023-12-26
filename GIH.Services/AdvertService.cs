using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;

namespace GIH.Services;

public class AdvertService : IAdvertService
{
    private readonly IRepositoryManager _repositoryManager;
  
    public AdvertService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public IQueryable<Advert> GetAdvert()
    {
        return _repositoryManager.AdvertRepository.GetAdvert();
    }

    public Advert GetAdvertById(int id)
    {
        return _repositoryManager.AdvertRepository.GetAdvertById(id);
    }

    public IEnumerable<Advert> GetAdvertByAdress(string address)
    {
        return _repositoryManager.AdvertRepository.GetAdvertByAdress(address);
    } 
    public void CreateAdvert(Advert advert)
    {
          if (advert is null)
        {
            throw new NullReferenceException(nameof(advert));
        }
        
        _repositoryManager.AdvertRepository.CreateAdvert(advert);
        _repositoryManager.Save();

    }
    public void UpdateAdvertById(int id, AdvertDto advertdto)
    {
        var entity = _repositoryManager.AdvertRepository.GetAdvertById(id);
        
        if (entity is null)
        {
            throw new Exception($"Advert with id : {id} could not found");
        }
        if (advertdto is null)
        {
            throw new Exception($"Advert is null");
        }

        //entity.AdvertId = advertdto.AdvertId;
        entity.AdvertName = advertdto.AdvertName;
        entity.AdvertDescription = advertdto.AdvertDescription;
        entity.AdvertKilo = advertdto.AdvertKilo;
        entity.AdvertDate = DateTime.UtcNow;
        

        _repositoryManager.AdvertRepository.UpdateAdvert(entity);
        _repositoryManager.Save();
    }
    public void DeleteAdvertById(int id)
    {
         var entity = _repositoryManager.AdvertRepository.GetAdvertById(id);
        if (entity is null)
        {
            throw new Exception($"Advert with id : {id} could not found");
        }
        _repositoryManager.AdvertRepository.DeleteAdvert(entity);
        _repositoryManager.Save();
    }

    
}
