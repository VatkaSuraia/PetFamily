namespace Contracts;

public sealed record VolunteerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string Email,
    string PhoneNumber,
    int ExperienceYears,
    string? Description,
    IReadOnlyList<SocialNetworkDto> SocialNetworks,
    IReadOnlyList<RequisitesDto> Requisites);

public sealed record PagedResponse<T>(
    IReadOnlyList<T> Elements,
    int Total,
    int Page,
    int PageSize);