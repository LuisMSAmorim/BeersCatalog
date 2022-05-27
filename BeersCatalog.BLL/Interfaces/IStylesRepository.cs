using BeersCatalog.BLL.Models;

namespace BeersCatalog.BLL.Interfaces;

public interface IStylesRepository
{
    Task AddAsync(Style style);
    Task<Style> GetAsync(int id);
    Task<List<Style>> GetAllAsync();
    Task UpdateAsync(int id, Style style);
    Task DeleteAsync(Style style);
}
