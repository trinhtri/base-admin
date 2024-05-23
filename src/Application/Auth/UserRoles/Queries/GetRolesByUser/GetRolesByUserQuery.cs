using Base.Application.Auth.UserRoles.Dtos;
using Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Base.Application.Auth.UserRoles.Queries.GetRolesByUser;
public record GetRolesByUserQuery(string UserId) : IRequest<List<UserRoleDto>>;

public class GetRolesByUserQueryHandler : IRequestHandler<GetRolesByUserQuery, List<UserRoleDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public GetRolesByUserQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<List<UserRoleDto>> Handle(GetRolesByUserQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = new List<UserRoleDto>();
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new ApplicationException("NotFound");

        foreach (var role in _roleManager.Roles.ToList())
        {
            var userRole = new UserRoleDto
            {
                RoleName = role.Name!
            };
            if (await _userManager.IsInRoleAsync(user, role.Name!))
            {
                userRole.Selected = true;
            }
            else
            {
                userRole.Selected = false;
            }
            result.Add(userRole);
        }
        return result;
    }
}
