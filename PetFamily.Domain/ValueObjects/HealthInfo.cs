using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public sealed class HealthInfo : ValueObject
{
    public HealthStatus Status { get; }
    public string Description { get; }

    public HealthInfo(HealthStatus status, string description, out UnitResult<Error> result)
    {
        result = Validate(status);
        if (result.IsFailure)
            return;
        
        Status = status;
        Description = description ?? string.Empty;
    }
    
    private UnitResult<Error> Validate(HealthStatus status)
    {
        if (!Enum.IsDefined(status))
            return Error.Validation("health.status.invalid", "Некорректный статус здоровья");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Status;
        yield return Description;
    }
}