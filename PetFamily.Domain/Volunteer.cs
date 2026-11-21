using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain;

public sealed class Volunteer : Entity
{
    private readonly List<SocialNetwork> _socialNetworks;
    private readonly List<Pet> _pets; // [W3] на этапе W2 не заполняется

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }             
    public int ExperienceYears { get; private set; }         
    public string Description { get; private set; }          
    public PhoneNumber PhoneNumber { get; private set; }              
    public Requisites Requisites { get; private set; }                 

    public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks.AsReadOnly();
    public IReadOnlyCollection<Pet> Pets => _pets.AsReadOnly();          // [W3]

    public Volunteer(
        long id,
        FullName fullName,
        Email email,
        int experienceYears,
        string description,
        PhoneNumber phoneNumber,
        Requisites requisites,
        IEnumerable<SocialNetwork>? socialNetworks = null)
        : base(id)
    {
        FullName = fullName;
        Email = email;
        ExperienceYears = experienceYears;
        Description = description;
        PhoneNumber = phoneNumber;
        Requisites = requisites;
        _socialNetworks = socialNetworks?.ToList() ?? new List<SocialNetwork>();
        _pets = new List<Pet>(); // [W3]
    }

    private Volunteer() { }

    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);

    // [W3] раскомментировать, когда питомцы будут связаны с волонтёром
    // public void AddPet(Pet pet) => _pets.Add(pet);
    public int GetFoundHomePetsCount(IReadOnlyCollection<Pet> pets)
        => pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);

    // которые ищут дом в данный момент
    public int GetSearchingForHomePetsCount(IReadOnlyCollection<Pet> pets)
        => pets.Count(p => p.HelpStatus == HelpStatus.SearchingForHome);

    // которые сейчас на лечении.
   
    public int GetUnderTreatmentPetsCount(IReadOnlyCollection<Pet> pets)
        => pets.Count(p => p.HealthInfo.Status == HealthStatus.UnderTreatment);
}