using System.Linq.Dynamic.Core;
using Base.Application.Common.Extensions.QueryableExtensions;
using Base.Application.Common.Mappings;
using Base.Application.Common.Models;
using Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Base.Application.Auth.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQuery : IRequest<PaginatedList<GetUsersResponse>>
{
    public string? FilterSearch { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Sorting { get; set; } = "UserName";
}

public record GetUsersResponse(string UserId, string? UserName, string? Email);

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<GetUsersResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public GetUsersWithPaginationQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<PaginatedList<GetUsersResponse>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _userManager.Users.WhereIf(!string.IsNullOrEmpty(request.FilterSearch),
                                x => x.UserName!.Contains(request.FilterSearch!.ToString()) ||
                                x.Email!.Contains(request.FilterSearch))
                                .OrderBy(request.Sorting)
                               .Select(x => new GetUsersResponse(x.Id, x.UserName, x.Email));
        return await query.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
