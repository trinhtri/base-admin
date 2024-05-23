using Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Base.Application.Auth.Users.Queries.GetUseById;

public record GetUseByIdQuery(string UserId) : IRequest<GetUseByIdQueryResponse>;

public record GetUseByIdQueryResponse(string UserId, string? UserName, string? Email);

public class GetUseByIdQueryHandler : IRequestHandler<GetUseByIdQuery, GetUseByIdQueryResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public GetUseByIdQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GetUseByIdQueryResponse> Handle(GetUseByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new AggregateException("NotFound");
        }
        var result = new GetUseByIdQueryResponse(user.Id, user.UserName, user.Email);
        return result;
    }
}
