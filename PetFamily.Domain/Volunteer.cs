using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain;

public sealed class Volunteer : Entity<Guid>
{
    private readonly List<SocialNetwork> _socialNetworks;
    private readonly List<Requisites> _requisites;
    private readonly List<Pet> _pets; // [W3] на этапе W2 не заполняется

    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }             
    public int ExperienceYears { get; private set; }         
    public string? Description { get; private set; }          
    public PhoneNumber PhoneNumber { get; private set; }              

    public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks.AsReadOnly();
    public IReadOnlyCollection<Requisites> Requisites => _requisites.AsReadOnly();
    public IReadOnlyCollection<Pet> Pets => _pets.AsReadOnly(); // [W3]
    
    public bool IsDeleted { get; private set; }
    public void Delete() => IsDeleted = true;


    private Volunteer(
        Guid id,
        FullName fullName,
        Email email,
        int experienceYears,
        string? description,
        PhoneNumber phoneNumber,
        IEnumerable<Requisites>? requisites = null,
        IEnumerable<SocialNetwork>? socialNetworks = null)
        : base(id)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        ExperienceYears = experienceYears;
        Description = description;
        PhoneNumber = phoneNumber;
        _requisites = requisites?.ToList() ?? new List<Requisites>();
        _socialNetworks = socialNetworks?.ToList() ?? new List<SocialNetwork>();
        _pets = new List<Pet>(); // [W3]
    }

    public static Result<Volunteer, Error> Create(
        Guid id,
        FullName fullName,
        Email email,
        int experienceYears,
        string? description,
        PhoneNumber phoneNumber,
        IEnumerable<Requisites>? requisites = null,
        IEnumerable<SocialNetwork>? socialNetworks = null)
    {
        if (id == Guid.Empty)
            return Error.Validation("value.is.invalid", "Id не может быть пустым");

        if (experienceYears < 0)
            return Error.Validation("value.is.invalid", "Id и опыт работы не могут быть отрицательными");

        if (fullName is null || email is null || phoneNumber is null || requisites is null)
            return Error.Validation("value.is.required", "ФИО, email, телефон и реквизиты обязательны для заполнения");

        return new Volunteer(
            id,
            fullName,
            email,
            experienceYears,
            description,
            phoneNumber,
            requisites,
            socialNetworks);
    }
    //для EF
    private Volunteer() { }

    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);
    public void AddRequisites(Requisites requisites) => _requisites.Add(requisites);
    public void AddPet(Pet pet) => _pets.Add(pet);
    
    public int GetFoundHomePetsCount(IReadOnlyCollection<Pet> pets)
        => pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);

    // которые ищут дом в данный момент
    public int GetSearchingForHomePetsCount(IReadOnlyCollection<Pet> pets)
        => pets.Count(p => p.HelpStatus == HelpStatus.SearchingForHome);

    // которые сейчас на лечении.
   
    public int GetUnderTreatmentPetsCount(IReadOnlyCollection<Pet> pets)
        => pets.Count(p => p.HealthInfo.Status == HealthStatus.UnderTreatment);
}