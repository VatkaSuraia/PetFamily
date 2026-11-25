using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    public Email(string value, out UnitResult<Error> result)
    {
        result = Validate(value);
        if (result.IsFailure)
            return;

        Value = value;
    }
    
    private UnitResult<Error> Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
            return Error.Validation("email.is.invalid", "Некорректный email");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}