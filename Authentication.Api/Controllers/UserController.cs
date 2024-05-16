using Authentication.Application.Users.Commands.DeleteUserCommand;
using Authentication.Application.Users.Commands.UpdateUserCommand;
using Authentication.Application.Users.Common;
using Authentication.Application.Users.Queries.GetAllUsersQuery;
using Authentication.Application.Users.Queries.GetUserByIdQuery;
using Authentication.Contracts.User;
using AutoMapper;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var query = new GetUserByIdQuery { Id = id };
        ErrorOr<UserResult> user = await _mediator.Send(query);

        return user.Match(
            user => Ok(_mapper.Map<UserResponse>(user)),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);
        if (result.IsError) return BadRequest(result.FirstError.Description);
        return Ok(result.Value);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var command = new DeleteUserCommand { Id = id };
        var result = await _mediator.Send(command);

        if (result.IsError) return BadRequest(result.FirstError.Description);

        return Ok("User deleted successfuly");
    }

    [HttpPut("{id:length(24)}")]

    [AllowAnonymous]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(id, request.FirstName, request.LastName);
        ErrorOr<UserResult> result = await _mediator.Send(command);

        return result.Match(
            user => Ok(_mapper.Map<UserResponse>(user)), 
            errors => Problem(errors)
        );
    }
}