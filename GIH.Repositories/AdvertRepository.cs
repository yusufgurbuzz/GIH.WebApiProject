using GIH.Entities;
using GIH.Interfaces.Repositories;

namespace GIH.Repositories;

public class AdvertRepository : RepositoryBase<Advert>, IAdvertRepository
{
    private readonly ApplicationDbContext _dbContext;
    public AdvertRepository(ApplicationDbContext context) : base(context)
    {
        _dbContext=context;
    }

    public void CreateAdvert(Advert advert)
    {
       Create(advert);
    }  
    
    public void UpdateAdvert(Advert advert)
    {
        Update(advert);
    }

    public void DeleteAdvert(Advert advert)
    {
        Delete(advert);
    }

    public IQueryable<Advert> GetAdvert()
    {
        return FindAll();
    }

    public IQueryable<Advert> GetAdvertByAdress(string adress)
    {
        // Adrese sahip restoranların Id'lerini çek
        var restaurantIds = _dbContext.Restaurants
            .Where(r => r.restaurantAdress == adress)
            .Select(r => r.restaurantId)
            .ToList();
        
        // Restoran Id'leri ile ilişkilendirilmiş ilanları çek
        var adverts = _dbContext.Adverts
            .Where(a => restaurantIds.Contains(a.RestaurantId));

        return adverts.AsQueryable();

    }

    public Advert GetAdvertById(int id)
    {
        return FindByCondition(b => b.AdvertId.Equals(id)).SingleOrDefault();
    }

  
}