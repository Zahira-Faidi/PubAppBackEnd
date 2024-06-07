using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Ads.Queries.GetAdById;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Ads.Queries.GetAdById
{
    public class GetAdByIdQueryHandlerTest
    {
        private readonly Mock<IAdRepository> _mockRepository;
        private readonly GetAdByIdQueryHandler _handler;

        public GetAdByIdQueryHandlerTest()
        {
            _mockRepository = new Mock<IAdRepository>();
            _handler = new GetAdByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsAd()
        {
            // Arrange
            var adId = "ad1";
            var expectedAd = new AdEntity { Id = adId, Name = "Test Ad" };
            var query = new GetAdByIdQuery(adId);

            _mockRepository.Setup(repo => repo.GetDetailsAsync(adId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(expectedAd);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(adId, result.Id);
            Assert.Equal("Test Ad", result.Name);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var adId = "ad2";
            var query = new GetAdByIdQuery(adId);

            _mockRepository.Setup(repo => repo.GetDetailsAsync(adId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((AdEntity)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
