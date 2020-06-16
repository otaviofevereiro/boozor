using AutoMapper;
using Curriculum.Business;
using Curriculum.Entities;

namespace Curriculum.Server.Controllers
{
    public class ExperiencesController : EntityController<Shared.Experience, Entities.Experience>
    {
        public ExperiencesController(IEntityService<Experience> entityService, IMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
