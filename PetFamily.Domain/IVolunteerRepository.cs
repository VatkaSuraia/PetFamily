namespace PetFamily.Domain;

public interface IVolunteerRepository
{
    Task AddAsync(Volunteer volunteer, CancellationToken ct = default);
    Task<Volunteer?> GetByIdAsync(Guid id, CancellationToken ct = default);
}