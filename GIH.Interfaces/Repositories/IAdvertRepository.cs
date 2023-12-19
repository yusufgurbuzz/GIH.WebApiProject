using GIH.Entities;

namespace GIH.Interfaces.Repositories;

public interface IAdvertRepository
{
    IQueryable<Advert> GetAdvert();
    Advert GetAdvertById(int id);
    IQueryable<Advert> GetAdvertByAdress(string adress);
    void CreateAdvert(Advert advert);
    void UpdateAdvert(Advert advert);
    void DeleteAdvert(Advert advert);
}