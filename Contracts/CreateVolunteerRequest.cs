namespace Contracts;

public sealed record SocialNetworkDto(string Name, string Url);
public sealed record RequisitesDto(string Name, string Value);

public record CreateVolunteerRequest
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    
    public string? MiddleName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public int ExperienceYears { get; init; }
    public string? Description { get; init; }
    public IEnumerable<SocialNetworkDto> SocialNetworks { get; init; } = [];
    public IEnumerable<RequisitesDto> Requisites { get; init; } = [];

}

public sealed record CreateVolunteerResponse(Guid Id);
public sealed record VolunteerResponse(Guid Id, string FirstName, string LastName, string? MiddleName,
    string Email, string PhoneNumber, int ExperienceYears, string? Description,
    IReadOnlyList<SocialNetworkDto> SocialNetworks, IReadOnlyList<RequisitesDto> Requisites);
public sealed record ErrorResponse(string Code, string Message);