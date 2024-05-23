namespace Base.Application.Common.Models;

public record LookupDto
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

}
