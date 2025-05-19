using MediatR;

namespace MyApp.Application.DTO.Products.Queries;

public record GetProductsQuery : IRequest<List<ProductDto>>;


