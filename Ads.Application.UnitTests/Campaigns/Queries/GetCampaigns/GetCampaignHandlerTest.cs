using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Campaigns.Queries.GetCampaignsQuery;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Campaigns.Queries.GetCampaigns
{
    public class GetCampaignsQueryHandlerTest
    {
        private readonly Mock<ICampaignRepository> _mockRepository;
        private readonly GetCampaignsQueryHandler _handler;

        public GetCampaignsQueryHandlerTest()
        {
            _mockRepository = new Mock<ICampaignRepository>();
            _handler = new GetCampaignsQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllCampaigns()
        {
            // Arrange
            var campaigns = new List<CampaignEntity>
            {
                new CampaignEntity { Id = "1", Name = "Campaign One" },
                new CampaignEntity { Id = "2", Name = "Campaign Two" }
            };
            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(campaigns);

            // Act
            var result = await _handler.Handle(new GetCampaignsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Id == "1" && c.Name == "Campaign One");
            Assert.Contains(result, c => c.Id == "2" && c.Name == "Campaign Two");
        }

        [Fact]
        public async Task Handle_NoCampaignsFound_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new List<CampaignEntity>());

            // Act
            var result = await _handler.Handle(new GetCampaignsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
