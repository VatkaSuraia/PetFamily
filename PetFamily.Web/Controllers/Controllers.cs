using Contracts;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Aplication;

namespace PetFamily.Web.Controllers;

[ApiController]
[Route("volunteers")]
public sealed class VolunteersController(VolunteerService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateVolunteerRequest request, CancellationToken ct)
    {
        var result = await service.CreateAsync(new CreateVolunteerCommand(request), ct);
        return result.IsSuccess
            ? Ok(new CreateVolunteerResponse(result.Value))                           // 200 + Id
            : BadRequest(new ErrorResponse(result.Error.Code, result.Error.Message)); // 400 + код/описание
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(new ErrorResponse(result.Error.Code, result.Error.Message));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var result = await service.GetPagedAsync(page, pageSize, ct);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(new ErrorResponse(result.Error.Code, result.Error.Message)); // 400
    }
}