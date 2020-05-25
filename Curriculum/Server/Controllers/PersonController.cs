using Curriculum.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curriculum.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        [HttpPost]
        public async Task<Person> PostAsync([FromBody] Person person)
        {
            throw new NotImplementedException();
        }
    }
}
