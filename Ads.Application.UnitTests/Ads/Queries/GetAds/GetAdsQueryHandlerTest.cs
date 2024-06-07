using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Ads.Queries.GetAds;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Ads.Queries.GetAds
{
    public class GetAdsQueryHandlerTest
    {
        private readonly Mock<IAdRepository> _mockRepository;
        private readonly GetAdsQueryHandler _handler;

        public GetAdsQueryHandlerTest()
        {
            _mockRepository = new Mock<IAdRepository>();
            _handler = new GetAdsQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllAds()
        {
            // Arrange
            var ads = new List<AdEntity>
            {
                new AdEntity { Id = "1", Name = "Ad One", IsDeleted = false },
                new AdEntity { Id = "2", Name = "Ad Two", IsDeleted = false }
            };
            var query = new GetAdsQuery { IsDeleted = false };

            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(ads);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, ad => ad.Name == "Ad One");
            Assert.Contains(result, ad => ad.Name == "Ad Two");
        }

        [Fact]
        public async Task Handle_NoAdsFound_ReturnsEmptyList()
        {
            // Arrange
            var query = new GetAdsQuery { IsDeleted = true };

            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new List<AdEntity>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
