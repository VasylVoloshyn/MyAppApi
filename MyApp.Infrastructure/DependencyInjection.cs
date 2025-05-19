using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using MyApp.Infrastructure.Identity;

namespace MyApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string? connectionString = null,
        bool useInMemory = false)
    {
        if (useInMemory)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
        }
        else
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
