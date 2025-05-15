using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Model;
using MyApp.Infrastructure.Persistence;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing"); // <--- оце важливо!

        builder.ConfigureServices(services =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();

            db.Products.AddRange(
                new Product { Name = "Mouse", Description = "Asus", Price = 15 },
                new Product { Name = "Monitor", Description = "LG", Price = 250 },
                new Product { Name = "Keyboard", Description = "HP", Price = 20 }
            );
            db.SaveChanges();
        });
    }
}
