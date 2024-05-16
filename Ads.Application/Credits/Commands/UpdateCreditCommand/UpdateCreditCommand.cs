using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Commands.UpdateCreditCommand;

public record UpdateCreditCommand(string Id, string Name, double AvailableCredit, double Consumed) : IRequest<CreditEntity>;