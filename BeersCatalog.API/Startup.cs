using BeersCatalog.API.Config;
using BeersCatalog.BLL.Interfaces;
using BeersCatalog.DAL;
using BeersCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BeersCatalog.API;
public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddDbContext<BeersCatalogDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<BeersCatalogDbContext>();

        var jwtSection = Configuration.GetSection("JwtBearerTokenSettings");
        services.Configure<JwtBearerTokenSettings>(jwtSection);

        var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
        var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtBearerTokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtBearerTokenSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    public void DependencyInjectionServices(IServiceCollection services)
    {
        services.AddScoped<IStylesRepository, StylesRepository>();
        services.AddScoped<IBeersRepository, BeersRepository>();
    }
}

