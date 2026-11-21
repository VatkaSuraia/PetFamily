using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public sealed record Breed : ValueObject
{
    public string Name { get; } 

    public Breed(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название породы не может быть пустым.", nameof(name));

        Name = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}