using BeersCatalog.BLL.Models;

namespace BeersCatalog.BLL.Interfaces;
public interface IBeersRepository
{
    Task AddAsync(Beer beer);
    Task<Beer> GetAsync(int id);
    Task<List<Beer>> GetAllAsync();
    Task<List<Beer>> GetAllByStyleAsync(Style style);
    Task UpdateAsync(int id, Beer beer);
    Task DeleteAsync(Beer beer);
}
