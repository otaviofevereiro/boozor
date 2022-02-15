using Curriculum.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Curriculum.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        public Person Get()
        {
            return new Person()
            {
                BirthDate = new DateTime(),
                Email = "otavio.fevereiro@outlook.com",
                Name = "Otavio Fevereiro",
            };

        }

        [HttpPost]
        public IActionResult Post(Person person)
        {

            return Ok();
        }
    }
}
