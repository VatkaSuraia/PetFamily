using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public sealed record Weight : ValueObject
{
    public double Value { get; }

    public Weight(double value)
    {
        if (value <= 0)
            throw new ArgumentException("Вес должен быть больше нуля.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}