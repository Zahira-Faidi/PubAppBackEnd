using Authentication.Application.Users.Commands.RegisterUserCommand;
using Authentication.Application.Users.Common;
using Authentication.Application.Users.Queries.LoginQuery;
using Authentication.Contracts.Authentication;
using Authentication.Contracts.User;
using AutoMapper;

namespace Authentication.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, RegisterUserCommand>();
            CreateMap<LoginRequest, LoginQuery>();
            CreateMap<AuthenticationResult, AuthenticationResponse>();
            CreateMap<UserResult, UserResponse>();
            CreateMap<UserRequest, UserResponse>();

        }
    }
}