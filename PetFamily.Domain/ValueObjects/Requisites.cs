using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;


public sealed class Requisites : ValueObject
{
    public string Name { get; }
    public string Description { get; }

    public Requisites(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название реквизитов обязательно.", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание перевода обязательно.", nameof(description));

        Name = name;
        Description = description;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Description;
    }
}