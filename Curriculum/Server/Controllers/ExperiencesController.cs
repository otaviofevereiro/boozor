using AutoMapper;
using Curriculum.Entities;
using DevPack.Data;

namespace Curriculum.Server.Controllers
{
    public class ExperiencesController : EntityController<Shared.Experience, Entities.Experience>
    {
        public ExperiencesController(IRepository<Experience> entityService, IMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
