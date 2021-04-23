using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            //Source -> Target
            CreateMap<District, DistrictReadDto>().ForMember(x => x.Province, opt => opt.MapFrom(x => x.Province.Name));
            CreateMap<DistrictWriteDto, District>().ForMember(x => x.ProvinceID, opt => opt.MapFrom(x => x.Province))
            .ForMember(x => x.Province, opt => opt.Ignore());
            CreateMap<District, DistrictWriteDto>();
        }
    }
}