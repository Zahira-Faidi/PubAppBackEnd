using Ads.Application.Products.Commands.UpdateProductCommand;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTest()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesProduct()
        {
            // Arrange
            var command = new UpdateProductCommand("1", "Updated Laptop", "image2.jpg", 1300.00, 15, 5, 100, "2", "2");
            var existingProduct = new ProductEntity { Id = "1", Name = "Laptop", Image = "image.jpg", Price = 1200.00, Quantity = 10, CPC = 3, Click = 50, CategoryId = "1", AdId = "1" };

            _mockRepository.Setup(r => r.GetDetailsAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingProduct);

            _mockRepository.Setup(r => r.UpdateAsync("1", It.IsAny<ProductEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingProduct);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Laptop", result.Name);
            Assert.Equal("image2.jpg", result.Image);
            Assert.Equal(1300.00, result.Price);
            Assert.Equal(15, result.Quantity);
            Assert.Equal(5, result.CPC);
            Assert.Equal(100, result.Click);
            Assert.Equal("2", result.CategoryId);
            Assert.Equal("2", result.AdId);
            _mockRepository.Verify(r => r.UpdateAsync("1", It.IsAny<ProductEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonexistentId_ThrowsException()
        {
            // Arrange
            var command = new UpdateProductCommand("2", "Updated Laptop", "image2.jpg", 1300.00, 15, 5, 100, "2", "2");

            _mockRepository.Setup(r => r.GetDetailsAsync("2", It.IsAny<CancellationToken>()))
                           .ReturnsAsync((ProductEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Product with id 2 not found", exception.Message);
        }
    }
}
