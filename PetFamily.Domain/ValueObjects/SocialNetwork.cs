using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public sealed class SocialNetwork : ValueObject
{
    public string Name { get; }
    public string Link { get; }
    
    public string Value => $"name: {Name}, link: {Link}"; 

    public SocialNetwork(string name, string link, out UnitResult<Error> result)
    {
        result = Validate(name, link);
        if (result.IsFailure)
            return;

        Name = name;
        Link = link;
    }
    
    private UnitResult<Error> Validate(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(link))
            return Error.Validation("social.is.invalid", "Название соцсети и ссылка обязательны");

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Link;
    }
}