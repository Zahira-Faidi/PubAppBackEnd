using Ads.Application.Categories.Commands.DeleteCategoryCommand;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandlerTest
    {
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly DeleteCategoryCommandHandler _handler;

        public DeleteCategoryCommandHandlerTest()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _handler = new DeleteCategoryCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidId_DeletesCategory()
        {
            // Arrange
            var command = new DeleteCategoryCommand("1");
            var category = new CategoryEntity { Id = "1", Name = "Technology" };

            _mockRepository.Setup(r => r.GetDetailsAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(category);

            _mockRepository.Setup(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(true));

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
            var command = new DeleteCategoryCommand("2");

            _mockRepository.Setup(r => r.GetDetailsAsync("2", It.IsAny<CancellationToken>()))
                           .ReturnsAsync((CategoryEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
