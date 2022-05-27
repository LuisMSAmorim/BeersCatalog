using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeersCatalog.BLL.Models;
using BeersCatalog.DAL;

namespace BeersCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly BeersCatalogDbContext _context;

        public BeersController(BeersCatalogDbContext context)
        {
            _context = context;
        }

        // GET: api/Beers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeer()
        {
            return await _context.Beer.ToListAsync();
        }

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            var beer = await _context.Beer.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }

        // PUT: api/Beers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            _context.Entry(beer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Beers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            _context.Beer.Add(beer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeer", new { id = beer.Id }, beer);
        }

        // DELETE: api/Beers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = await _context.Beer.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beer.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeerExists(int id)
        {
            return _context.Beer.Any(e => e.Id == id);
        }
    }
}
