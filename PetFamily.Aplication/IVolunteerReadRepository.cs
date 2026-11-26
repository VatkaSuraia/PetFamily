using Contracts;

namespace PetFamily.Aplication;

public interface IVolunteerReadRepository
{
    Task<VolunteerDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResponse<VolunteerDto>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
}