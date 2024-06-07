using Ads.Application.Campaigns.Commands.DeleteCampaign;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Domain.Enums;
using FluentAssertions;
using MediatR;
using Moq;

namespace Ads.Application.UnitTests.Campaigns.Commands.DeleteCampaign
{
    public class DeleteCampaignCommandHandlerTests
    {
        private readonly Mock<ICampaignRepository> _mockRepository;
        private readonly DeleteCampaignCommandHandler _handler;

        public DeleteCampaignCommandHandlerTests()
        {
            _mockRepository = new Mock<ICampaignRepository>();
            _handler = new DeleteCampaignCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_CampaignNotFound_ThrowsException()
        {
            // Arrange
            var command = new DeleteCampaignCommand("nonexistent_id");
            _mockRepository.Setup(repo => repo.GetDetailsAsync(command.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((CampaignEntity?)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Campaign with ID nonexistent_id not found.");
        }

        [Theory]
        [InlineData(Status.Active)]
        [InlineData(Status.Inactive)]
        public async Task Handle_CampaignActiveOrInactive_ThrowsException(Status status)
        {
            // Arrange
            var campaign = new CampaignEntity { Id = "6645d97a143ecf7d4fc23645", Status = status };
            var command = new DeleteCampaignCommand(campaign.Id);
            _mockRepository.Setup(repo => repo.GetDetailsAsync(command.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new CampaignEntity { Id = campaign.Id, Status = campaign.Status });

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("You can't delete a Campaign with Active or Inactive Status");
        }

        [Fact]
        public async Task Handle_ValidRequest_DeletesCampaign()
        {
            // Arrange
            var campaign = new CampaignEntity { Id = "6645d97a143ecf7d4fc23645", Status = Status.InDraft };
            var command = new DeleteCampaignCommand(campaign.Id);
            _mockRepository.Setup(repo => repo.GetDetailsAsync(command.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(campaign);
            _mockRepository.Setup(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true); // Fixed to return a Task<bool> instead of Task

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            _mockRepository.Verify(repo => repo.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
