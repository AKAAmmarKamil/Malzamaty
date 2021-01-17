using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            //Source -> Target
            CreateMap <Roles, RolesReadDto> ();
            CreateMap<RolesWriteDto, Roles>();
            CreateMap<Roles, RolesWriteDto>();
        }
    }
}