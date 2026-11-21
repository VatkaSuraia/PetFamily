using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

// ФИО волонтёра
public sealed record FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? MiddleName { get; }

    public FullName(string firstName, string lastName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Имя обязательно.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Фамилия обязательна.", nameof(lastName));

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}