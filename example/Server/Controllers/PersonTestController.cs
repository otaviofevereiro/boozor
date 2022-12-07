using Example.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Example.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    [HttpPost]
    public ActionResult Post(Person person)
    {
        return Ok();
    }
}