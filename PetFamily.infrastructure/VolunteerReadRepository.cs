using System.Linq.Expressions;
using Contracts;
using Microsoft.EntityFrameworkCore;
using PetFamily.Aplication;
using PetFamily.Domain;

namespace PetFamily.infrastructure;

public sealed class VolunteerReadRepository(ReadDbContext db) : IVolunteerReadRepository
{
    // одна проекция на оба запроса — клиенту только нужные поля
    private static readonly Expression<Func<Volunteer, VolunteerDto>> ToDto = v => new VolunteerDto(
        v.Id,
        v.FullName.FirstName, v.FullName.LastName, v.FullName.MiddleName,
        v.Email.Value, v.PhoneNumber.Value,
        v.ExperienceYears, v.Description,
        v.SocialNetworks.Select(s => new SocialNetworkDto(s.Name, s.Link)).ToList(),
        v.Requisites.Select(r => new RequisitesDto(r.Name, r.Description)).ToList());

    public Task<VolunteerDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Volunteers.AsNoTracking()
            .Where(v => v.Id == id && !v.IsDeleted)   // удалённый = 404
            .Select(ToDto)
            .FirstOrDefaultAsync(ct);

    public async Task<PagedResponse<VolunteerDto>> GetPagedAsync(
        int page, int pageSize, CancellationToken ct = default)
    {
        var query = db.Volunteers.AsNoTracking().Where(v => !v.IsDeleted);

        var total = await query.CountAsync(ct);       // только неудалённые, в БД
        var elements = await query
            .OrderBy(v => v.Id)                       // единый порядок → страницы не пересекаются
            .Skip((page - 1) * pageSize)              // пагинация в SQL, не в памяти
            .Take(pageSize)
            .Select(ToDto)
            .ToListAsync(ct);

        return new PagedResponse<VolunteerDto>(elements, total, page, pageSize);
    }
}