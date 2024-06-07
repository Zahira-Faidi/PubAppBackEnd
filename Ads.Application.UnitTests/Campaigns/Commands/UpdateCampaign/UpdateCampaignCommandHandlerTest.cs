using System;
using System.Threading;
using System.Threading.Tasks;
using Ads.Application.Campaigns.Commands.UpdateCampaignCommand;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Ads.Application.UnitTests.Campaigns.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandlerTest
    {
        private readonly Mock<ICampaignRepository> _mockRepository;
        private readonly UpdateCampaignCommandHandler _handler;

        public UpdateCampaignCommandHandlerTest()
        {
            _mockRepository = new Mock<ICampaignRepository>();
            _handler = new UpdateCampaignCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ShouldUpdateCampaign()
        {
            // Arrange
            var command = new UpdateCampaignCommand("1", "New Campaign", DateTimeOffset.Now, DateTimeOffset.Now.AddDays(10), 1000, "B1", 500.0);
            var campaign = new CampaignEntity { Id = "1", Name = "Old Campaign" };

            _mockRepository.Setup(repo => repo.GetDetailsAsync(command.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(campaign);
            _mockRepository.Setup(repo => repo.UpdateAsync(command.Id, It.IsAny<CampaignEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(campaign);  // Return the campaign object as part of the Task

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("New Campaign", result.Name);
            _mockRepository.Verify(repo => repo.UpdateAsync(command.Id, It.IsAny<CampaignEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ShouldThrowException()
        {
            // Arrange
            var command = new UpdateCampaignCommand("2", "New Campaign", DateTimeOffset.Now, DateTimeOffset.Now.AddDays(10), 1000, "B1", 500.0);

            _mockRepository.Setup(repo => repo.GetDetailsAsync(command.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((CampaignEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}