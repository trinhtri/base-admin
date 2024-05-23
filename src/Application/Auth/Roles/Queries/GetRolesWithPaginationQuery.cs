using Base.Application.Common.Extensions.QueryableExtensions;
using Base.Application.Common.Mappings;
using Base.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Base.Application.Auth.Roles.Queries;

public record GetRolesWithPaginationQuery : IRequest<PaginatedList<GetRolesResponse>>
{
    public string? FilterSearch { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public record GetRolesResponse(string Id, string Name);

public class GetRolesWithPaginationQueryHandler : IRequestHandler<GetRolesWithPaginationQuery, PaginatedList<GetRolesResponse>>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetRolesWithPaginationQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<PaginatedList<GetRolesResponse>> Handle(GetRolesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _roleManager.Roles
            .WhereIf(!string.IsNullOrEmpty(request.FilterSearch), x => x.Name!.Contains(request.FilterSearch!))
            .Select(x => new GetRolesResponse(x.Id, x.Name!));
        return await query.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
