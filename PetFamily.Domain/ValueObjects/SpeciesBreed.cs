using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public abstract class SpeciesBreed : ValueObject
{
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }

    public SpeciesBreed(Guid speciesId, Guid breedId, out UnitResult<Error> result)
    {
        result = Validate(speciesId, breedId);
        if (result.IsFailure)
            return;

        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    private UnitResult<Error> Validate(Guid speciesId, Guid breedId)
    {
        if (speciesId == Guid.Empty || breedId == Guid.Empty)
            return Error.Validation("species.breed.invalid", "Id вида и породы не могут быть пустыми");

        return UnitResult.Success<Error>();
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SpeciesId;
        yield return BreedId;
    } 
}