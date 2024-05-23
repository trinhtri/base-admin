using Base.Application.Auth.Roles.Queries;
using Base.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Base.Web.Controllers.Auth;

[Route("api/roles")]
public class RolesController : ApiControllerBase
{

    [HttpGet("{id}")]
    public async Task<GetRoleByIdQueryResponse> GetById([FromRoute] string id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetRoleByIdQuery(id), cancellationToken);
    }

    [HttpGet]
    public async Task<PaginatedList<GetRolesResponse>> GetAll([FromQuery] GetRolesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }
}
