using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Commands.CreateCreditCommand;

public record CreateCreditCommand(string Name, double AvailableCredit, double Consumed) : IRequest<CreditEntity>;
