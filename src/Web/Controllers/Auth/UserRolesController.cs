using Base.Application.Auth.UserRoles.Commands.UpdateUserRolesByUser;
using Base.Application.Auth.UserRoles.Dtos;
using Base.Application.Auth.UserRoles.Queries.GetRolesByUser;
using Microsoft.AspNetCore.Mvc;

namespace Base.Web.Controllers.Auth;

[Route("api/user-roles")]
public class UserRolesController : ApiControllerBase
{
    [HttpPut()]
    public async Task UpdateUserRole(UpdateUserRolesByUserCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
    }

    [HttpGet("{userId}")]
    public async Task<List<UserRoleDto>> GetUserRoles([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var request = new GetRolesByUserQuery(userId);
        var result = await Mediator.Send(request, cancellationToken);
        return result;
    }
}
