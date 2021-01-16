using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            //Source -> Target
            CreateMap <Match, MatchReadDto > ().ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name)).ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name));

            CreateMap<MatchWriteDto, Match>()
                .ForMember(x => x.C_ID, opt => opt.MapFrom(x => x.Class)).ForMember(x => x.Su_ID, opt => opt.MapFrom(x => x.Subject)).ForMember(x => x.Class, opt => opt.Ignore())
                .ForMember(x => x.Subject, opt => opt.Ignore());
            CreateMap<Match,MatchWriteDto >();
        }
    }
}