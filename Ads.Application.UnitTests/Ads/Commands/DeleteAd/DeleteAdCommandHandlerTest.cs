using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Ads.Commands.DeleteAd;
using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Ads.Commands.DeleteAd
{
    public class DeleteAdCommandHandlerTest
    {
        private readonly Mock<IAdRepository> _mockRepository;
        private readonly DeleteAdCommandHandler _handler;

        public DeleteAdCommandHandlerTest()
        {
            _mockRepository = new Mock<IAdRepository>();
            _handler = new DeleteAdCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_AdExists_UpdatesIsDeleted()
        {
            // Arrange
            var adId = "ad1";
            var ad = new AdEntity { Id = adId, IsDeleted = false };
            var command = new DeleteAdCommand(adId);

            _mockRepository.Setup(repo => repo.GetDetailsAsync(adId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(ad);
            _mockRepository.Setup(repo => repo.UpdateAsync(adId, It.IsAny<AdEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(ad);  // Assuming UpdateAsync returns the updated AdEntity

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            Assert.True(ad.IsDeleted);
            _mockRepository.Verify(repo => repo.UpdateAsync(adId, ad, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_AdDoesNotExist_ThrowsAdNotFoundException()
        {
            // Arrange
            var adId = "ad2";
            var command = new DeleteAdCommand(adId);

            _mockRepository.Setup(repo => repo.GetDetailsAsync(adId, It.IsAny<CancellationToken>())).ReturnsAsync((AdEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<AdNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
