using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

// ФИО волонтёра
public sealed class FullName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? MiddleName { get; }

    public FullName(string firstName, string lastName, out UnitResult<Error> result, string? middleName = null)
    {
        result = Validate(firstName, lastName);
        if (result.IsFailure)
            return;

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
    
    private UnitResult<Error> Validate(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            return Error.Validation("fullname.is.invalid", "Имя и фамилия обязательны");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName?? string.Empty;
    }
}