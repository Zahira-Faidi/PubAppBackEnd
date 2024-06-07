using Ads.Application.Products.Queries.GetProductByIdQuery;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using ErrorOr;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Ads.Domain.Common.Errors;

namespace Ads.Application.UnitTests.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandlerTest
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTest()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new GetProductByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ExistingId_ReturnsProduct()
        {
            // Arrange
            var productId = "1";
            var expectedProduct = new ProductEntity { Id = productId, Name = "Laptop" };
            var query = new GetProductByIdQuery(productId);

            _mockRepository.Setup(r => r.GetDetailsAsync(productId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(expectedProduct);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expectedProduct, result.Value);
        }

        [Fact]
        public async Task Handle_NonExistingId_ReturnsError()
        {
            // Arrange
            var productId = "2";
            var query = new GetProductByIdQuery(productId);

            _mockRepository.Setup(r => r.GetDetailsAsync(productId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((ProductEntity)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(Errors.Global.IdNotFound.Code, result.Errors[0].Code);
        }
    }
}
