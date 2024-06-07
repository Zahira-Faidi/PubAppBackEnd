using Ads.Application.Budgets.Commands.UpdateBudgetCommand;
using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Budgets.Commands.UpdateBudget
{
    public class UpdateBudgetCommandHandlerTest
    {
        private readonly Mock<IBudgetRepository> _mockRepository;
        private readonly UpdateBudgetCommandHandler _handler;

        public UpdateBudgetCommandHandlerTest()
        {
            _mockRepository = new Mock<IBudgetRepository>();
            _handler = new UpdateBudgetCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesBudget()
        {
            // Arrange
            var command = new UpdateBudgetCommand("6644d886911727f9e9686acd", "Updated Budget", 1500);
            var existingBudget = new BudgetEntity { Id = "6644d886911727f9e9686acd", Name = "Original Budget", TotalBudget = 1000 };

            _mockRepository.Setup(r => r.GetDetailsAsync("6644d886911727f9e9686acd", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingBudget);

            _mockRepository.Setup(r => r.UpdateAsync("6644d886911727f9e9686acd", It.IsAny<BudgetEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new BudgetEntity { Id = "6644d886911727f9e9686acd", Name = "Updated Budget", TotalBudget = 1500});

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Budget", result.Name);
            Assert.Equal(1500, result.TotalBudget);
            _mockRepository.Verify(r => r.UpdateAsync("6644d886911727f9e9686acd", It.IsAny<BudgetEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NonexistentId_ThrowsBudgetNotFoundException()
        {
            // Arrange
            var command = new UpdateBudgetCommand("6644d886911727f9e9686acd", "Nonexistent Budget", 1500);

            _mockRepository.Setup(r => r.GetDetailsAsync("6644d886911727f9e9686acd", It.IsAny<CancellationToken>()))
                           .ReturnsAsync((BudgetEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<BudgetNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
