using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Source -> Target
            CreateMap<User, UserReadDto>().ForMember(x=>x.Roles,opt=>opt.MapFrom(x=>x.Roles.Role));
            CreateMap<InterestReadDto, UserReadDto>();
            CreateMap<UserWriteDto, User>();
            CreateMap<User,UserWriteDto >();
        }
    }
}
