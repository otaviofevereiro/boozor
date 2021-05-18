using AutoMapper;
using Curriculum.Entities;
using DevPack.Data;

namespace Curriculum.Server.Controllers
{
    public class PersonsController : EntityController<Shared.Person, Entities.Person>
    {
        public PersonsController(IRepository<Person> entityService, IMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
