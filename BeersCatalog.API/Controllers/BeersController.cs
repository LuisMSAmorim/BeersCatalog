using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeersCatalog.BLL.Models;
using BeersCatalog.DAL;
using BeersCatalog.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BeersCatalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BeersController : ControllerBase
{
    private readonly IBeersRepository _repository;

    public BeersController
    (
        IBeersRepository beersRepository
    )
    {
        _repository = beersRepository;
    }

    // GET: api/Beers
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Beer>>> GetBeer()
    {
        return await _repository.GetAllAsync();
    }

    // GET: api/Beers/styles/1
    [HttpGet]
    [Route("styles/{styleId}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Beer>>> GetBeersByStyleId(int styleId)
    {
        return await _repository.GetAllByStyleAsync(styleId);
    }

    // GET: api/Beers/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Beer>> GetBeer(int id)
    {
        var beer = await _repository.GetAsync(id);

        if (beer == null)
        {
            return NotFound();
        }

        return beer;
    }

    // PUT: api/Beers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutBeer(int id, Beer beerData)
    {
        if (id != beerData.BeerId)
        {
            return BadRequest();
        }

        var beer = await _repository.GetAsync(id);

        if (beer == null)
        {
            return NotFound();
        }

        await _repository.UpdateAsync(id, beerData);

        return NoContent();
    }

    // POST: api/Beers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Beer>> PostBeer(Beer beer)
    {
        await _repository.AddAsync(beer);

        return CreatedAtAction("GetBeer", new { id = beer.BeerId });
    }

    // DELETE: api/Beers/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBeer(int id)
    {
        var beer = await _repository.GetAsync(id);

        if (beer == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(beer);

        return NoContent();
    }
}
