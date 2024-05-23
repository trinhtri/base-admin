using Microsoft.AspNetCore.Identity;

namespace Base.Application.Auth.Roles.Queries;

public record GetRoleByIdQuery(string Id) : IRequest<GetRoleByIdQueryResponse>;

public record GetRoleByIdQueryResponse(string Id, string? Name);

public class GetRoleClaimsByRoleIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleByIdQueryResponse>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetRoleClaimsByRoleIdQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id);
        if (role == null)
        {
            throw new AggregateException("NotFound");
        }
        var result = new GetRoleByIdQueryResponse(role.Id, role.Name);
        return result;
    }
}
