using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using MyApp.Application.DTO.Products.Queries;

namespace MyApp.API.Tests;

[TestClass]
public class ProductApiTests
{
    private readonly HttpClient _client;

    public ProductApiTests()
    {
        var factory = new CustomWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [TestMethod]
    public async Task Get_Products_Returns_Success_And_Data()
    {
        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        var actual = JsonSerializer.Deserialize<List<ProductDto>>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var expected = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Mouse", Description = "Asus", Price = 15 },
            new ProductDto { Id = 2, Name = "Monitor", Description = "LG", Price = 250 },
            new ProductDto { Id = 3, Name = "Keyboard", Description = "HP", Price = 20 },
        };

        CollectionAssert.AreEqual(expected, actual, new ProductComparer());

    }
}

