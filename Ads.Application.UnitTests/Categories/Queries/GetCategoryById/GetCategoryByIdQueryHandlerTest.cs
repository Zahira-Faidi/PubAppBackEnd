using Ads.Application.Categories.Queries.GetCategoryByIdQuery;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandlerTest
    {
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly GetCategoryByIdQueryHandler _handler;

        public GetCategoryByIdQueryHandlerTest()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _handler = new GetCategoryByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ExistingId_ReturnsCategory()
        {
            // Arrange
            var categoryId = "1";
            var expectedCategory = new CategoryEntity { Id = categoryId, Name = "Technology" };
            var query = new GetCategoryByIdQuery(categoryId);

            _mockRepository.Setup(r => r.GetDetailsAsync(categoryId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(expectedCategory);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCategory, result);
        }

        [Fact]
        public async Task Handle_NonExistingId_ReturnsNull()
        {
            // Arrange
            var categoryId = "2";
            var query = new GetCategoryByIdQuery(categoryId);

            _mockRepository.Setup(r => r.GetDetailsAsync(categoryId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((CategoryEntity)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
