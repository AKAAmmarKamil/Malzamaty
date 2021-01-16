using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class InterestProfile : Profile
    {
        public InterestProfile()
        {
            //Source -> Target
            CreateMap <Interests, InterestReadDto > ().ForMember(x=>x.UserName,opt=>opt.MapFrom(x=>x.User.UserName))
            .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.ClassName)).ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name));

            CreateMap<InterestWriteDto, Interests>().ForMember(x => x.U_ID, opt => opt.MapFrom(x => x.User))
                .ForMember(x => x.C_ID, opt => opt.MapFrom(x => x.Class)).ForMember(x => x.Su_ID, opt => opt.MapFrom(x => x.Subject)).ForMember(x => x.User, opt => opt.Ignore()).ForMember(x => x.Class, opt => opt.Ignore())
                .ForMember(x => x.Subject, opt => opt.Ignore());
            CreateMap<Interests,InterestWriteDto >();
        }
    }
}