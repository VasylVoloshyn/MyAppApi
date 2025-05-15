using MediatR;

namespace MyApp.Application.Products.Queries;

public record GetProductsQuery : IRequest<List<ProductDto>>;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

}
