using Ads.Application.Budgets.Commands.DeleteBudget;
using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Budgets.Commands.DeleteBudget
{
    public class DeleteBudgetCommandHandlerTest
    {
        private readonly Mock<IBudgetRepository> _mockRepository;
        private readonly DeleteBudgetCommandHandler _handler;

        public DeleteBudgetCommandHandlerTest()
        {
            _mockRepository = new Mock<IBudgetRepository>();
            _handler = new DeleteBudgetCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ShouldDeleteBudget()
        {
            // Arrange
            var command = new DeleteBudgetCommand("1");
            var budget = new BudgetEntity { Id = "1", Name = "Marketing", TotalBudget = 1000 };

            _mockRepository.Setup(r => r.GetDetailsAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(budget);

            _mockRepository.Setup(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _mockRepository.Verify(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ShouldThrowBudgetNotFoundException()
        {
            // Arrange
            var command = new DeleteBudgetCommand("2");

            _mockRepository.Setup(r => r.GetDetailsAsync("2", It.IsAny<CancellationToken>()))
                           .ReturnsAsync((BudgetEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<BudgetNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}