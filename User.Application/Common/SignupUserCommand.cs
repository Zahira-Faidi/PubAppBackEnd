using MediatR;

namespace User.Application.Common
{
    public class SignupUserCommand : IRequest<string>
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
