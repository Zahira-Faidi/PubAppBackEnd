using Ads.Application.Budgets.Commands.CreateBudget;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Budgets.Commands.CreateBudget
{
    public class CreateBudgetCommandHandlerTest
    {
        private readonly Mock<IBudgetRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateBudgetCommandHandler _handler;

        public CreateBudgetCommandHandlerTest()
        {
            _mockRepository = new Mock<IBudgetRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateBudgetCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateBudget()
        {
            // Arrange
            var command = new CreateBudgetCommand("Marketing", 1000);
            var budget = new BudgetEntity { Name = "Marketing", TotalBudget = 1000 };

            _mockMapper.Setup(m => m.Map<BudgetEntity>(It.IsAny<CreateBudgetCommand>()))
                       .Returns(budget);

            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<BudgetEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(budget);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Marketing", result.Name);
            Assert.Equal(1000, result.TotalBudget);
            _mockRepository.Verify(r => r.InsertAsync(budget, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<BudgetEntity>(command), Times.Once);
        }
    }
}