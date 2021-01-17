using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            //Source -> Target
            CreateMap <Subject, SubjectReadDto > ();
            CreateMap<SubjectWriteDto, Subject>();
            CreateMap<Subject,SubjectWriteDto >();
        }
    }
}