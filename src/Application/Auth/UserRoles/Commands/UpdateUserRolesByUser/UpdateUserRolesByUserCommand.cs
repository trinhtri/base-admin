using Base.Application.Auth.UserRoles.Dtos;
using Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Base.Application.Auth.UserRoles.Commands.UpdateUserRolesByUser;

public record UpdateUserRolesByUserCommand(string UserId, List<UserRoleDto> UserRoles) : IRequest<Unit>;

public class UpdateRoleClaimsByRoleIdCommandHandler : IRequestHandler<UpdateUserRolesByUserCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UpdateRoleClaimsByRoleIdCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Unit> Handle(UpdateUserRolesByUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            throw new ApplicationException("NotFound");

        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        result = await _userManager.AddToRolesAsync(user, request.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
        return Unit.Value;
    }
}
