using Microsoft.EntityFrameworkCore;
using PetFamily.Domain;
using PetFamily.infrastructure;

namespace PetFamily.Infrastructure;

public class VolunteerRepository(ApplicationDbContext db) : IVolunteerRepository
{
    public async Task AddAsync(Volunteer volunteer, CancellationToken ct = default)
    {
        db.Volunteers.Add(volunteer);
        await db.SaveChangesAsync(ct);
    }

    public Task<Volunteer?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Volunteers.FirstOrDefaultAsync(v => v.Id == id, ct);
}