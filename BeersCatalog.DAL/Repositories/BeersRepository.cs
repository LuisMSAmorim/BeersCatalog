using BeersCatalog.BLL.Interfaces;
using BeersCatalog.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BeersCatalog.DAL.Repositories;

public sealed class BeersRepository : IBeersRepository
{
    private readonly BeersCatalogDbContext _context;

    public BeersRepository(BeersCatalogDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Beer beer)
    {
        _context.Add(beer);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Beer beer)
    {
        _context.Remove(beer);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Beer>> GetAllAsync()
    {
        return await _context.Beer.ToListAsync();
    }

    public async Task<List<Beer>> GetAllByStyleAsync(Style style)
    {
        return await _context.Beer.Where(x => x.Style == style).ToListAsync();
    }

    public async Task<Beer> GetAsync(int id)
    {
        return await _context.Beer.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(int id, Beer beer)
    {
        _context.Update(beer);

        await _context.SaveChangesAsync();

        var oldBeer = await _context.Beer.SingleOrDefaultAsync(beer => beer.Id == id);

        _context.Entry(oldBeer).CurrentValues.SetValues(beer);

        await _context.SaveChangesAsync();
    }
}
