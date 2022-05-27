using Microsoft.AspNetCore.Mvc;
using BeersCatalog.BLL.Models;
using BeersCatalog.BLL.Interfaces;

namespace BeersCatalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StylesController : ControllerBase
{
    private readonly IStylesRepository _context;

    public StylesController
    (
        IStylesRepository stylesRepository
    )
    {
        _context = stylesRepository;
    }

    // GET: api/Styles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Style>>> GetStyle()
    {
        return await _context.GetAllAsync();
    }

    // GET: api/Styles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Style>> GetStyle(int id)
    {
        var style = await _context.GetAsync(id);

        if (style == null)
        {
            return NotFound();
        }

        return style;
    }

    // PUT: api/Styles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStyle(int id, Style styleData)
    {
        if (id != styleData.Id)
        {
            return BadRequest();
        }

        var style = await _context.GetAsync(id);

        if (style == null)
        {
            return NotFound();
        }

        await _context.UpdateAsync(id, styleData);

        return NoContent();
    }

    // POST: api/Styles
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Style>> PostStyle(Style style)
    {
        await _context.AddAsync(style);

        return CreatedAtAction("GetStyle", new { id = style.Id }, style);
    }

    // DELETE: api/Styles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStyle(int id)
    {
        var style = await _context.GetAsync(id);

        if (style == null)
        {
            return NotFound();
        }

        await _context.DeleteAsync(style);

        return NoContent();
    }
}
