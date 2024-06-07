using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Ads.Commands.CreateAd;
using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Ads.Commands.CreateAd
{
    public class CreateAdCommandHandlerTest
    {
        private readonly Mock<IAdRepository> _mockAdRepository;
        private readonly Mock<IBudgetRepository> _mockBudgetRepository;
        private readonly Mock<ICampaignRepository> _mockCampaignRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CreateAdCommandHandler>> _mockLogger;
        private readonly CreateAdCommandHandler _handler;

        public CreateAdCommandHandlerTest()
        {
            _mockAdRepository = new Mock<IAdRepository>();
            _mockBudgetRepository = new Mock<IBudgetRepository>();
            _mockCampaignRepository = new Mock<ICampaignRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CreateAdCommandHandler>>();
            _handler = new CreateAdCommandHandler(_mockAdRepository.Object, _mockMapper.Object, _mockBudgetRepository.Object, _mockCampaignRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesAd()
        {
            // Arrange
            var command = new CreateAdCommand("Ad Name", DateTimeOffset.Now, DateTimeOffset.Now.AddDays(10), "Campaign1", 100);
            var campaign = new CampaignEntity { Id = "Campaign1", BudgetId = "Budget1", Consumed = 0 };
            var budget = new BudgetEntity { Id = "Budget1", TotalBudget = 500 };
            var ad = new AdEntity { Name = "Ad Name", CampaignId = "Campaign1", Credit = 100 };

            _mockCampaignRepository.Setup(repo => repo.GetDetailsAsync("Campaign1", It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(campaign);
            _mockBudgetRepository.Setup(repo => repo.GetDetailsAsync("Budget1", It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(budget);
            _mockAdRepository.Setup(repo => repo.GetAllAdsByCampaignId("Campaign1", It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new List<AdEntity>());
            _mockMapper.Setup(m => m.Map<AdEntity>(It.IsAny<CreateAdCommand>()))
                       .Returns(ad);
            _mockAdRepository.Setup(repo => repo.InsertAsync(It.IsAny<AdEntity>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(ad);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ad Name", result.Name);
            _mockCampaignRepository.Verify(repo => repo.UpdateAsync("Campaign1", It.IsAny<CampaignEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_BudgetExceeded_ThrowsException()
        {
            // Arrange
            var command = new CreateAdCommand("Ad Name", DateTimeOffset.Now, DateTimeOffset.Now.AddDays(10), "Campaign1", 600);
            var campaign = new CampaignEntity { Id = "Campaign1", BudgetId = "Budget1", Consumed = 0 };
            var budget = new BudgetEntity { Id = "Budget1", TotalBudget = 500 };

            _mockCampaignRepository.Setup(repo => repo.GetDetailsAsync("Campaign1", It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(campaign);
            _mockBudgetRepository.Setup(repo => repo.GetDetailsAsync("Budget1", It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(budget);
            _mockAdRepository.Setup(repo => repo.GetAllAdsByCampaignId("Campaign1", It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new List<AdEntity>());

            // Act & Assert
            await Assert.ThrowsAsync<BudgetExceededException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
