using MyApp.Application.Products.Queries;


namespace MyApp.API.Tests;

public class ProductComparer : Comparer<ProductDto>
{
    public override int Compare(ProductDto x, ProductDto y)
    {
        if (x == null || y == null) return -1;
        if (x.Id != y.Id) return -1;
        if (x.Name != y.Name) return -1;
        if (x.Description != y.Description) return -1;
        if (x.Price != y.Price) return -1;
        return 0;
    }
}
