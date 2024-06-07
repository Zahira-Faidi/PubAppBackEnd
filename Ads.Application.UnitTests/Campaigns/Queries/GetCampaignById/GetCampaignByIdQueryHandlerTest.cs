using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Campaigns.Queries.GetCampaignByIdQuery;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Campaigns.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandlerTest
    {
        private readonly Mock<ICampaignRepository> _mockRepository;
        private readonly GetCampaignByIdQueryHandler _handler;

        public GetCampaignByIdQueryHandlerTest()
        {
            _mockRepository = new Mock<ICampaignRepository>();
            _handler = new GetCampaignByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsCampaign()
        {
            // Arrange
            var campaignId = "1";
            var campaign = new CampaignEntity { Id = campaignId, Name = "Test Campaign" };
            var query = new GetCampaignByIdQuery(campaignId);

            _mockRepository.Setup(repo => repo.GetDetailsAsync(campaignId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(campaign);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(campaignId, result.Id);
            Assert.Equal("Test Campaign", result.Name);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var campaignId = "2";
            var query = new GetCampaignByIdQuery(campaignId);

            _mockRepository.Setup(repo => repo.GetDetailsAsync(campaignId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((CampaignEntity?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
