using Authentication.Application.DTOs;
using Authentication.Application.Roles.Commands.AssignUserRoleCommand;
using Authentication.Application.Users.Commands.RegisterUserCommand;
using Authentication.Application.Users.Queries.GetUserByIdQuery;
using Authentication.Application.Users.Queries.LoginQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Authentication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
                return BadRequest("Invalid email or password");
            return Ok(result);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(AssignUserRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var query = new GetUserByIdQuery { UserId = userId };
            var user = await _mediator.Send(query);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
