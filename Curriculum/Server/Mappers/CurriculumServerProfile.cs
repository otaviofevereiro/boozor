using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curriculum.Server.Mappers
{
    public class CurriculumServerProfile : Profile
    {
        public CurriculumServerProfile()
        {
            CreateMap<Entities.Person, Shared.Person>().ReverseMap();
            CreateMap<Entities.Experience, Shared.Experience>().ReverseMap();
        }
    }
}
