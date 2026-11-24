using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public sealed class Breed : Entity<Guid>
{
    // Конструктор для EF Core
    private Breed() { }
    
    public string Name { get; private set;}

    private Breed(Guid id, string name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Breed, Error> Create(Guid id, string name)
    {
        if (id == Guid.Empty || string.IsNullOrWhiteSpace(name))
            return Error.Validation("value.is.invalid", "Id и название породы не могут быть пустыми");

        return new Breed(id, name);
    }
}