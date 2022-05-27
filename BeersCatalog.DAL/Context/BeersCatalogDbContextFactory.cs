using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BeersCatalog.DAL;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BeersCatalogDbContext>
{
    public BeersCatalogDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory()
            + "/../BeersCatalog.API/appsettings.json").Build();

        var builder = new DbContextOptionsBuilder<BeersCatalogDbContext>();
        var connectionString = configuration.GetConnectionString("DatabaseConnection");

        builder.UseSqlServer(connectionString);

        return new BeersCatalogDbContext(builder.Options);
    }
}
