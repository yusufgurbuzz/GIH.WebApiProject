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

    public IEnumerable<Advert> GetAdvertByAdress(string adress)
    {
        //adrese göre restorantIdsini aldık.
        var restaurantSameAdress = _context.Restaurants
            .Where(r => r.restaurantAdress == adress)
            .ToList();
        //RestorantId lerini listeye koyduk.
        var restaurantIds = restaurantSameAdress.Select(r => r.restaurantId).ToList();

        //İlanlardaki restorantIdleri karşılaştırıp aynı olanları listeye aldık.
        var adverts = _context.Adverts
            .Where(i => restaurantIds.Contains(i.RestaurantId))
            .ToList();

        // Veritabanından alınan verileri dilimledik
        adverts = adverts.Where(i => restaurantIds.Contains(i.RestaurantId))
                         .ToList();

        return adverts;
    }

    public Advert GetAdvertById(int id)
    {
        return FindByCondition(b => b.AdvertId.Equals(id)).SingleOrDefault();
    }

  
}