using System.Drawing;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain;

public sealed class Pet : Entity<Guid>
{ 
    public Guid Id { get; private set; }
    public string Name { get; private set; }               
    public SpeciesBreed SpeciesBreed { get; private set; }             
    public string? Description { get; private set; }      
    public Breed Breed { get; private set; }          
    public Color Color { get; private set; }         
    public HealthInfo HealthInfo { get; private set; }      
    public Address Address { get; private set; }      
    public Weight Weight { get; private set; }          
    public Height Height { get; private set; }          
    public PhoneNumber OwnerPhone { get; private set; }    
    public bool IsSterilized { get; private set; }        
    public DateTime BirthDate { get; private set; }         
    public bool IsVaccinated { get; private set; }        
    public HelpStatus HelpStatus { get; private set; } 
    public Requisites Requisites { get; private set; }      
    public DateTime CreatedAt { get; private set; }     
    
    private Pet(
        Guid id,
        string name,
        SpeciesBreed speciesBreed,
        string? description,
        Breed breed,
        Color color,
        HealthInfo healthInfo,
        Address address,
        Weight weight,
        Height height,
        PhoneNumber ownerPhone,
        bool isSterilized,
        DateTime birthDate,
        bool isVaccinated,
        HelpStatus helpStatus,
        Requisites requisites,
        DateTime createdAt)
        : base(id)
    {
        Id = id;
        Name = name;
        SpeciesBreed = speciesBreed;
        Description = description;
        Breed = breed;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        Weight = weight;
        Height = height;
        OwnerPhone = ownerPhone;
        IsSterilized = isSterilized;
        BirthDate = birthDate;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        Requisites = requisites;
        CreatedAt = createdAt;
    }

    public static Result<Pet, Error> Create(
        Guid id,
        string name,
        SpeciesBreed speciesBreed,
        string? description,
        Breed breed,
        Color color,
        HealthInfo healthInfo,
        Address address,
        Weight weight,
        Height height,
        PhoneNumber ownerPhone,
        bool isSterilized,
        DateTime birthDate,
        bool isVaccinated,
        HelpStatus helpStatus,
        Requisites requisites,
        DateTime createdAt)
    {
        if (Guid.Empty == id || string.IsNullOrEmpty(name))
            return Error.Validation("value.is.invalid", "Некорректный Id или пустое имя питомца");

        if (speciesBreed is null || breed is null || color == default ||
            healthInfo is null || address is null || weight is null ||
            height is null || ownerPhone is null || requisites is null || 
            !Enum.IsDefined(helpStatus))
        {
            return Error.Validation("value.is.required", "Все характеристики и реквизиты питомца обязательны");
        }

        if (birthDate > DateTime.UtcNow || createdAt == default)
            return Error.Validation("date.is.invalid", "Указана некорректная дата рождения или создания");

        return new Pet(
            id,
            name,
            speciesBreed,
            description,
            breed,
            color,
            healthInfo,
            address,
            weight,
            height,
            ownerPhone,
            isSterilized,
            birthDate,
            isVaccinated,
            helpStatus,
            requisites,
            createdAt);
    }

    // Конструктор без id — для случаев, когда id генерируется позже (опционально)
    private Pet() { }
}