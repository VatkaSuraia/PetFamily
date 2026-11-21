using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain;

public sealed class Pet : Entity
{
    public string Name { get; private set; }               
    public Species Species { get; private set; }             
    public string Description { get; private set; }      
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

    public Pet(
        long id,
        string name,
        Species species,
        string description,
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
        Name = name;
        Species = species;
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

    // Конструктор без id — для случаев, когда id генерируется позже (опционально)
    private Pet() { }
}