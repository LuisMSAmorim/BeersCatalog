using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BeersCatalog.Presentation.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BeersCatalogPresentationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeersCatalogPresentationContext") ?? throw new InvalidOperationException("Connection string 'BeersCatalogPresentationContext' not found.")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
