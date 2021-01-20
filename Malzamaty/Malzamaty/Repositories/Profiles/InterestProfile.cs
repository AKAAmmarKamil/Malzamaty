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
            .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name)).ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name));

            CreateMap<InterestWriteDto, Interests>().ForMember(x => x.UserID, opt => opt.Ignore())
                .ForMember(x => x.ClassID, opt => opt.MapFrom(x => x.Class)).ForMember(x => x.SubjectID, opt => opt.MapFrom(x => x.Subject)).ForMember(x => x.User, opt => opt.Ignore()).ForMember(x => x.Class, opt => opt.Ignore())
                .ForMember(x => x.Subject, opt => opt.Ignore());
            CreateMap<Interests,InterestWriteDto >();
        }
    }
}