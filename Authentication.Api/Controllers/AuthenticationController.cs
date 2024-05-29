using Authentication.Application.Roles.Commands.AssignUserRoleCommand;
using Authentication.Application.Users.Commands.RegisterUserCommand;
using Authentication.Application.Users.Common;
using Authentication.Application.Users.Queries.LoginQuery;
using Authentication.Contracts.Authentication;
using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Authentication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ApiController 
{ 
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors)
        );

    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
             errors => Problem(errors)
        );

    }


    [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(AssignUserRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

    public static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                        authResult.User.Id,
                        authResult.User.FirstName,
                        authResult.User.LastName,
                        authResult.User.Email,
                        authResult.User.Role,
                        authResult.User.Image,
                        authResult.User.Status,
                        authResult.Token
                    );
    }
}
