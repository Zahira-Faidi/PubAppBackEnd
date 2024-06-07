using Ads.Application.Products.Queries.GetProductsQuery;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Products.Queries.GetProducts
{
    public class GetProductsQueryHandlerTest
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly GetProductsQueryHandler _handler;

        public GetProductsQueryHandlerTest()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new GetProductsQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<ProductEntity>
            {
                new ProductEntity { Id = "1", Name = "Laptop" },
                new ProductEntity { Id = "2", Name = "Smartphone" }
            };
            var query = new GetProductsQuery();

            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(products);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Laptop");
            Assert.Contains(result, p => p.Name == "Smartphone");
        }
    }
}
