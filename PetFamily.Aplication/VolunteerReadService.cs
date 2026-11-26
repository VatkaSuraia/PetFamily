using Contracts;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Aplication;

public sealed class VolunteerReadService
{
    private VolunteerReadService(IVolunteerReadRepository reads)
    {
        _reads = reads;
    }

    private IVolunteerReadRepository _reads { get; set; }
    
    public async Task<Result<VolunteerDto, Error>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var dto = await _reads.GetByIdAsync(id, ct);
        return dto is null
            ? Error.NotFound(code: "volunteer.not_found", message: "Волонтёр не найден")
            : dto;
    }

    public async Task<Result<PagedResponse<VolunteerDto>, Error>> GetPagedAsync(
        int page, int pageSize, CancellationToken ct = default)
    {
        if (page < 1)
            return Error.Validation(code: "page.invalid", message: "page должен быть >= 1");
        if (pageSize is < 1 or > 100)
            return Error.Validation(code: "page_size.invalid", message: "pageSize должен быть от 1 до 100");

        return await _reads.GetPagedAsync(page, pageSize, ct);
    }
}