using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Campaigns.Queries.GetCampaignByIdSellerQuery;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Campaigns.Queries.GetCampaignByIdSeller
{
    public class GetCampaignByIdSellerQueryHandlerTest
    {
        private readonly Mock<ICampaignRepository> _mockRepository;
        private readonly GetCampaignByIdSellerQueryHandler _handler;

        public GetCampaignByIdSellerQueryHandlerTest()
        {
            _mockRepository = new Mock<ICampaignRepository>();
            _handler = new GetCampaignByIdSellerQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidSellerId_ReturnsCampaigns()
        {
            // Arrange
            var sellerId = "seller123";
            var campaigns = new List<CampaignEntity>
            {
                new CampaignEntity { Id = "1", Name = "Campaign One", SellerId = sellerId },
                new CampaignEntity { Id = "2", Name = "Campaign Two", SellerId = sellerId }
            };
            var query = new GetCampaignByIdSellerQuery(sellerId);

            _mockRepository.Setup(repo => repo.GetCampaignsBySeller(sellerId))
                           .ReturnsAsync(campaigns);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, item => Assert.Equal(sellerId, item.SellerId));
        }

        [Fact]
        public async Task Handle_NoCampaignsFound_ReturnsEmptyList()
        {
            // Arrange
            var sellerId = "seller123";
            var query = new GetCampaignByIdSellerQuery(sellerId);

            _mockRepository.Setup(repo => repo.GetCampaignsBySeller(sellerId))
                           .ReturnsAsync(new List<CampaignEntity>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}