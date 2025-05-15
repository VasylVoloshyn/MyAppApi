using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Model;

namespace MyApp.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
