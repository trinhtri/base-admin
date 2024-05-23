namespace Base.Application.Auth.Users.Queries.GetByCurrentUser;
public record GetUserByCurentUserQueryResponse
{
    public required string Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}
