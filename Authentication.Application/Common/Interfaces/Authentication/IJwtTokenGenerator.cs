using Authentication.Domain.Entities;

namespace Authentication.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
