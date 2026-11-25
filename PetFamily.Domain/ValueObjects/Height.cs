using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public sealed class Height : ValueObject
{
    public double Value { get; }

    public Height(double value, out UnitResult<Error> result)
    {
        result = Validate(value);
        if (result.IsFailure)
            return;

        Value = value;
    }
    
    private UnitResult<Error> Validate(double value)
    {
        if (value <= 0)
            return Error.Validation("height.is.invalid", "Рост должен быть больше нуля");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}