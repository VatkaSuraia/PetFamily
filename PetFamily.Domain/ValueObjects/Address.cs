using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

// Адрес, где находится питомец
public sealed class Address : ValueObject
{
    public string City { get; }
    public string Street { get; }
    public string House { get; }

    public Address(string city, string street, string house)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Город обязателен.", nameof(city));
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Улица обязательна.", nameof(street));
        if (string.IsNullOrWhiteSpace(house))
            throw new ArgumentException("Дом обязателен.", nameof(house));

        City = city;
        Street = street;
        House = house;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
        yield return House;
    }
}