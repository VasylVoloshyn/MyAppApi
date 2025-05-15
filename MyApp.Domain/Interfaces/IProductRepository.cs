using MyApp.Domain.Model;

namespace MyApp.Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
}
