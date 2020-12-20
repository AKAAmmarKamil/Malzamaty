using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class ClassTypeProfile : Profile
    {
        public ClassTypeProfile()
        {
            //Source -> Target
            CreateMap <ClassType, ClassTypeReadDto> ();
            CreateMap<ClassTypeWriteDto, ClassType>();
            CreateMap<ClassType, ClassTypeWriteDto>();
        }
    }
}