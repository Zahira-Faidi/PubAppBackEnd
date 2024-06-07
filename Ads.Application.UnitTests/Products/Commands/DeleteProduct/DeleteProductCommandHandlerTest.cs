using Ads.Application.Products.Commands.DeleteProductCommand;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTest()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidId_DeletesProduct()
        {
            // Arrange
            var command = new DeleteProductCommand("1");
            var product = new ProductEntity { Id = "1", Name = "Laptop" };

            _mockRepository.Setup(r => r.GetDetailsAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(product);

            _mockRepository.Setup(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _mockRepository.Verify(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonexistentId_ThrowsException()
        {
            // Arrange
            var command = new DeleteProductCommand("2");

            _mockRepository.Setup(r => r.GetDetailsAsync("2", It.IsAny<CancellationToken>()))
                           .ReturnsAsync((ProductEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
