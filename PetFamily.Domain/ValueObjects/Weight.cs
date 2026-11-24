using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public sealed class Weight : ValueObject
{
    public double Value { get; }

    public Weight(double value, out UnitResult<Error> result)
    {
        result = Validate(value);
        if (result.IsFailure)
            return;

        Value = value;
    }
    
    private UnitResult<Error> Validate(double value)
    {
        if (value <= 0)
            return Error.Validation("weight.is.invalid", "Вес должен быть больше нуля");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}