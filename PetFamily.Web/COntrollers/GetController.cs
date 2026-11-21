using Microsoft.AspNetCore.Mvc;

namespace PetFamily.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GetController : ControllerBase
{
    // GET api/get
    [HttpGet]
    [ProducesResponseType(204)]
    public IResult Get()
    {
        return Results.NoContent();
    }
}