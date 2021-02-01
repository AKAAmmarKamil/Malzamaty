using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            //Source -> Target
            CreateMap<Class, ClassReadDto>().ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Stage.Name)).ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.ClassType.Name)
            ).ForMember(x => x.Country, opt => opt.MapFrom(x => x.Country.Name));
            CreateMap<ClassWriteDto, Class>().ForMember(x => x.StageID, opt => opt.MapFrom(x => x.Stage))
               .ForMember(x => x.ClassTypeID, opt => opt.MapFrom(x => x.ClassType)).ForMember(x => x.CountryID, opt => opt.MapFrom(x => x.Country)).ForMember(x => x.Stage, opt => opt.Ignore()).ForMember(x => x.ClassType, opt => opt.Ignore())
               .ForMember(x => x.Country, opt => opt.Ignore());
            CreateMap<Class, ClassWriteDto>();
        }
    }
}