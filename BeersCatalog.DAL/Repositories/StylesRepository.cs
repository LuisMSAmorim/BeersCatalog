using BeersCatalog.BLL.DTOs;
using BeersCatalog.BLL.Interfaces;
using BeersCatalog.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BeersCatalog.DAL.Repositories;

public sealed class StylesRepository : IStylesRepository
{
    private readonly BeersCatalogDbContext _context;
    
    public StylesRepository(BeersCatalogDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Style style)
    {
        _context.Add(style);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Style style)
    {
        _context.Remove(style);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Style>> GetAllAsync()
    {
        return await _context.Style.ToListAsync();
    }

    public async Task<Style> GetAsync(int id)
    {
        return await _context.Style.SingleOrDefaultAsync(style => style.StyleId == id);
    }

    public async Task UpdateAsync(int id, StyleDTO style)
    {
        var oldStyle = await _context.Style.SingleOrDefaultAsync(style => style.StyleId == id);

        _context.Entry(oldStyle).CurrentValues.SetValues(style);

        await _context.SaveChangesAsync();
    }
}
