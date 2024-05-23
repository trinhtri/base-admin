using Base.Application.Auth.Users.Queries.GetByCurrentUser;
using Base.Application.Auth.Users.Queries.GetUseById;
using Base.Application.Auth.Users.Queries.GetUsersWithPagination;
using Base.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Base.Web.Controllers.Auth;

[Route("api/users")]
[Authorize]
public class UsersController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<GetUseByIdQueryResponse> GetById([FromRoute] string id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUseByIdQuery(id), cancellationToken);
    }
    [HttpGet("current-user")]
    public async Task<GetUserByCurentUserQueryResponse> GetById(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetUserByCurentUserQuery(), cancellationToken);
    }

    [HttpGet]
    public async Task<PaginatedList<GetUsersResponse>> GetAll([FromQuery] GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return result;
    }
}
