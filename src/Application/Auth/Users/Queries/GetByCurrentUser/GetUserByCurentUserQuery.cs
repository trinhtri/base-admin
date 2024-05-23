using Base.Application.Common.Interfaces;
using Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Base.Application.Auth.Users.Queries.GetByCurrentUser;

public record GetUserByCurentUserQuery() : IRequest<GetUserByCurentUserQueryResponse>;

public class GetUserByCurentUserQueryHandler : IRequestHandler<GetUserByCurentUserQuery, GetUserByCurentUserQueryResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUser _currentUser;
    public GetUserByCurentUserQueryHandler(
        UserManager<ApplicationUser> userManager,
        ICurrentUser currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public async Task<GetUserByCurentUserQueryResponse> Handle(GetUserByCurentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_currentUser.Id ?? "");
        if (user == null)
        {
            throw new AggregateException("NotFound");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var result = new GetUserByCurentUserQueryResponse()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Roles = roles.ToList(),
        };
        return result;
    }
}
