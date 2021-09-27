using AutoMapper;
using Curriculum.Data;
using Curriculum.Entities;
using DevPack.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Curriculum.Server.Controllers
{
    public class PersonsController : EntityController<Shared.Person, Entities.Person>
    {
        public PersonsController(IRepository<Person> repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
