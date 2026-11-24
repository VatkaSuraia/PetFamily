using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;


public sealed class Requisites : ValueObject
{
    public string Name { get; }
    public string Description { get; }

    public Requisites(string name, string description, out UnitResult<Error> result)
    {
        result = Validate(name, description);
        if (result.IsFailure)
            return;

        Name = name;
        Description = description;
    }
    
    private UnitResult<Error> Validate(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            return Error.Validation("requisites.is.invalid", "Название и описание реквизитов обязательны");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Description;
    }
}