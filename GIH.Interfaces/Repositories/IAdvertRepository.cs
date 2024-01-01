using GIH.Entities;

namespace GIH.Interfaces.Repositories;

public interface IAdvertRepository
{
    IQueryable<Advert> GetAdvert();
    Advert GetAdvertById(int id);
    IEnumerable<Advert> GetAdvertByAdress(string adress);
    IEnumerable<Advert> GetAdvertByRestaurantId(int id);
    void CreateAdvert(Advert advert);
    void UpdateAdvert(Advert advert);
    void DeleteAdvert(Advert advert);
}