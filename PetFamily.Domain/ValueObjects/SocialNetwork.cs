using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;


public sealed record SocialNetwork : ValueObject
{
    public string Name { get; }
    public string Link { get; }

    public SocialNetwork(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название соцсети обязательно.", nameof(name));
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("Ссылка обязательна.", nameof(link));

        Name = name;
        Link = link;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Link;
    }
}