using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain;

public sealed class Species : Entity<Guid>
{
    // Конструктор для EF Core
    private Species() { }
    
    public string Title { get; private set;}

    private List<Breed> _breeds { get; } = [];
    //Это факту лист для публичного чтения
    public IReadOnlyList<Breed> Breeds => _breeds.AsReadOnly();
    
    public Species(Guid id, string title) : base(id)
    {
        Title = title;
    }
    
    public static Result<Species, Error> Create(Guid id, string title)
    {
        if (id == Guid.Empty || string.IsNullOrWhiteSpace(title))
            return Error.Validation("value.is.invalid", "Id и название вида не могут быть пустыми");

        return new Species(id, title);
    }
    
    public UnitResult<Error> AddBreed(Breed breed)
    {
        if (breed is null)
            return Error.Validation("breed.is.null", "Порода не может быть пустой");

        if (_breeds.Any(b => b.Name == breed.Name))
            return Error.Conflict("breed.already.exists", "Такая порода уже существует у этого вида");

        _breeds.Add(breed);

        return UnitResult.Success<Error>();
    }
}