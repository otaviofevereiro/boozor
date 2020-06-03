using AutoMapper;
using Curriculum.Business;
using Curriculum.Entities;

namespace Curriculum.Server.Controllers
{
    public class PersonsController : EntityController<Shared.Person, Entities.Person>
    {
        public PersonsController(IEntityService<Person> entityService, IMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
