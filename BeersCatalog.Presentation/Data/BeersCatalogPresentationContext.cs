using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeersCatalog.BLL.Models;

namespace BeersCatalog.Presentation.Data
{
    public class BeersCatalogPresentationContext : DbContext
    {
        public BeersCatalogPresentationContext (DbContextOptions<BeersCatalogPresentationContext> options)
            : base(options)
        {
        }

        public DbSet<BeersCatalog.BLL.Models.Beer>? Beer { get; set; }
    }
}
