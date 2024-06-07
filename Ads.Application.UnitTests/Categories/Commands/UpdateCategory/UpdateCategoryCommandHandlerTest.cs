using Ads.Application.Categories.Commands.UpdateCategoryCommand;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandlerTest
    {
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly UpdateCategoryCommandHandler _handler;

        public UpdateCategoryCommandHandlerTest()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _handler = new UpdateCategoryCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesCategory()
        {
            // Arrange
            var command = new UpdateCategoryCommand("1", "Updated Name");
            var existingCategory = new CategoryEntity { Id = "1", Name = "Original Name" };

            _mockRepository.Setup(r => r.GetDetailsAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingCategory);

            _mockRepository.Setup(r => r.UpdateAsync("1", It.IsAny<CategoryEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingCategory);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
            _mockRepository.Verify(r => r.UpdateAsync("1", It.IsAny<CategoryEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonexistentId_ThrowsException()
        {
            // Arrange
            var command = new UpdateCategoryCommand("2", "Updated Name");

            _mockRepository.Setup(r => r.GetDetailsAsync("2", It.IsAny<CancellationToken>()))
                           .ReturnsAsync((CategoryEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Category with id 2 not found", exception.Message);
        }
    }
}
