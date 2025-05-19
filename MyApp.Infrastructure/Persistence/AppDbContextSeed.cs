using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Model;

namespace MyApp.Infrastructure.Persistence;

public static class AppDbContextSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product {  Name = "Mouse", Description = "Asus", Price = 15 },
                new Product { Name = "Monitor", Description = "LG", Price = 250 },
                new Product { Name = "Keyboard", Description = "HP" , Price = 20}
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }       
    }

    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = new[] { "Client", "Manager" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
