using Ads.Application.Categories.Queries.GetCategoriesQuery;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandlerTest
    {
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly GetCategoriesQueryHandler _handler;

        public GetCategoriesQueryHandlerTest()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _handler = new GetCategoriesQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<CategoryEntity>
            {
                new CategoryEntity { Id = "1", Name = "Technology" },
                new CategoryEntity { Id = "2", Name = "Health" }
            };
            var query = new GetCategoriesQuery();

            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(categories);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Id == "1" && c.Name == "Technology");
            Assert.Contains(result, c => c.Id == "2" && c.Name == "Health");
        }
    }
}
