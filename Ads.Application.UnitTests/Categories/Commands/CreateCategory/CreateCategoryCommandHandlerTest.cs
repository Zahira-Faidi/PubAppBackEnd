using Ads.Application.Categories.Commands.CreateCategoryCommand;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandlerTest
    {
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateCategoryCommandHandler _handler;

        public CreateCategoryCommandHandlerTest()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateCategoryCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateCategory()
        {
            // Arrange
            var command = new CreateCategoryCommand("Technology");
            var category = new CategoryEntity { Name = "Technology" };

            _mockMapper.Setup(m => m.Map<CategoryEntity>(It.IsAny<CreateCategoryCommand>()))
                       .Returns(category);

            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<CategoryEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(category);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Technology", result.Name);
            _mockRepository.Verify(r => r.InsertAsync(category, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<CategoryEntity>(command), Times.Once);
        }
    }
}
