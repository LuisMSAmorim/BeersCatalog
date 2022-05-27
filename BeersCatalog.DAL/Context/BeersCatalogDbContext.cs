using BeersCatalog.BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeersCatalog.DAL;

public class BeersCatalogDbContext : IdentityDbContext<IdentityUser>
{
    public BeersCatalogDbContext(DbContextOptions<BeersCatalogDbContext> options) : base(options) { }

    public virtual DbSet<Style> Style { get; set; }
    public virtual DbSet<Beer> Beer { get; set; }
}
