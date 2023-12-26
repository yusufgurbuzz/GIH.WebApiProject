using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Repositories;

namespace GIH.Interfaces.Services;

public interface IAdvertService
{
    IQueryable<Advert> GetAdvert();
    Advert GetAdvertById(int id);
    IEnumerable<Advert> GetAdvertByAdress(string address);
    void CreateAdvert(Advert advert);
    void UpdateAdvertById(int id,AdvertDto advertdto);
    void DeleteAdvertById(int id);
}