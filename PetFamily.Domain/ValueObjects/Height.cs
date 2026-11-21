using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public sealed record Height : ValueObject
{
    public double Value { get; }

    public Height(double value)
    {
        if (value <= 0)
            throw new ArgumentException("Рост должен быть больше нуля.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}