using Ads.Application.Products.Commands.CreateProductCommand;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTest()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateProductCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateProduct()
        {
            // Arrange
            var command = new CreateProductCommand("Laptop", "image.jpg", 1200.00, 10, "1", "1");
            var product = new ProductEntity { Name = "Laptop", Image = "image.jpg", Price = 1200.00, Quantity = 10, CategoryId = "1", AdId = "1" };

            _mockMapper.Setup(m => m.Map<ProductEntity>(It.IsAny<CreateProductCommand>()))
                       .Returns(product);

            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<ProductEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(product);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Laptop", result.Name);
            Assert.Equal("image.jpg", result.Image);
            Assert.Equal(1200.00, result.Price);
            Assert.Equal(10, result.Quantity);
            Assert.Equal("1", result.CategoryId);
            Assert.Equal("1", result.AdId);
            _mockRepository.Verify(r => r.InsertAsync(product, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<ProductEntity>(command), Times.Once);
        }
    }
}
