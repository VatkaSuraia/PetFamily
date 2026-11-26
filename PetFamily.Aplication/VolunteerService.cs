using Contracts;
using CSharpFunctionalExtensions;
using PetFamily.Domain;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Aplication;

public class VolunteerService(IVolunteerRepository repository, IVolunteerReadRepository reads)
{
    public async Task<Result<Guid, Error>> CreateAsync(CreateVolunteerCommand command, CancellationToken ct = default)
    {
        var r = command.Request;

        var fullName = new FullName(r.FirstName, null, r.LastName, out var fnResult);
        if (fnResult.IsFailure) return fnResult.Error;
        var email = new Email(r.Email, out var eResult);
        if (eResult.IsFailure) return eResult.Error;
        var phone = new PhoneNumber(r.PhoneNumber, out var pResult);
        if (pResult.IsFailure) return pResult.Error;

        var socials = new List<SocialNetwork>();
        foreach (var s in r.SocialNetworks ?? [])
        {
            var social = new SocialNetwork(s.Name, s.Url, out var socialResult);
            if (socialResult.IsFailure) return socialResult.Error;
            socials.Add(social);
        }

        var requisites = new List<Requisites>();
        foreach (var x in r.Requisites ?? [])
        {
            var requisite = new Requisites(x.Name, x.Value, out var reqResult);
            if (reqResult.IsFailure) return reqResult.Error;
            requisites.Add(requisite);
        }
        
        
        var volunteer = Volunteer.Create(
            Guid.NewGuid(),
            fullName,
            email,
            r.ExperienceYears,
            r.Description,
            phone,
            requisites,
            socials);
        if (volunteer.IsFailure) return volunteer.Error;

        await repository.AddAsync(volunteer.Value, ct);
        return volunteer.Value.Id;
    }

    public async Task<Result<VolunteerResponse, Error>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var v = await repository.GetByIdAsync(id, ct);
        if (v is null) return Error.NotFound(code: "volunteer.not_found", message: "Волонтёр не найден");

        return new VolunteerResponse(v.Id,
            v.FullName.FirstName, v.FullName.LastName, v.FullName.MiddleName,
            v.Email.Value, v.PhoneNumber.Value,
            v.ExperienceYears, v.Description,
            v.SocialNetworks.Select(s => new SocialNetworkDto(s.Name, s.Link)).ToList(),
            v.Requisites.Select(x => new RequisitesDto(x.Name, x.Description)).ToList());
    }
    
    public async Task<Result<PagedResponse<VolunteerDto>, Error>> GetPagedAsync(
        int page, int pageSize, CancellationToken ct = default)
    {
        if (page < 1)
            return Error.Validation(code: "page.invalid", message: "page должен быть >= 1");
        if (pageSize is < 1 or > 100)
            return Error.Validation(code: "page_size.invalid", message: "pageSize должен быть от 1 до 100");

        return await reads.GetPagedAsync(page, pageSize, ct);
    }

    private static Result<List<TVo>> MapList<TDto, TVo>(IEnumerable<TDto>? items, Func<TDto, Result<TVo>> create)
    {
        var list = new List<TVo>();
        foreach (var item in items ?? [])
        {
            var result = create(item);
            if (result.IsFailure) return Result.Failure<List<TVo>>(result.Error!);
            list.Add(result.Value);
        }
        return Result.Success(list);
    }
}