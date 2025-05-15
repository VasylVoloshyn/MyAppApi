using MediatR;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _repo;

    public GetProductsQueryHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _repo.GetAllAsync();
        return products.Select(u => new ProductDto
        {
            Id = u.Id,
            Name = u.Name,
            Description = u.Description,
            Price = u.Price
        }).ToList();
    }
}

