using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

// Адрес, где находится питомец
public sealed class Address : ValueObject
{
    public string City { get; }
    public string Street { get; }
    public string House { get; }

    public Address(string city, string street, string house, out UnitResult<Error> result)
    {
        result = Validate(city, street, house);
        if (result.IsFailure)
            return;

        City = city;
        Street = street;
        House = house;
    }
    
    private UnitResult<Error> Validate(string city, string street, string house)
    {
        if (string.IsNullOrWhiteSpace(city) || 
            string.IsNullOrWhiteSpace(street) || 
            string.IsNullOrWhiteSpace(house))
        {
            return Error.Validation("address.is.invalid", "Город, улица и дом обязательны");
        }

        return UnitResult.Success<Error>();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
        yield return House;
    }
}