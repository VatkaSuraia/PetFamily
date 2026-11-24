using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string Value { get; }

    public PhoneNumber(string value, out UnitResult<Error> result)
    {
        result = Validate(value);
        if (result.IsFailure)
            return;

        Value = value;
    }
    
    private UnitResult<Error> Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.Validation("phone.is.invalid", "Номер телефона не может быть пустым");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}