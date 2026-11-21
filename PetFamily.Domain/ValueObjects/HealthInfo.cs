using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;

namespace PetFamily.Domain.ValueObjects;

public sealed record HealthInfo : ValueObject
{
    public HealthStatus Status { get; }
    public string Description { get; }

    public HealthInfo(HealthStatus status, string description)
    {
        Status = status;
        Description = description ?? string.Empty;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Status;
        yield return Description;
    }
}