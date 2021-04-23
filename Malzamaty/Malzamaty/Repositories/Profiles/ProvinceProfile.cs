using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile()
        {
            //Source -> Target
            CreateMap<Province, ProvinceReadDto>().ForMember(x => x.Country, opt => opt.MapFrom(x => x.Country.Name));
            CreateMap<ProvinceWriteDto, Province>().ForMember(x => x.CountryID, opt => opt.MapFrom(x => x.Country))
            .ForMember(x => x.Country, opt => opt.Ignore());
            CreateMap<Province, ProvinceWriteDto>();
        }
    }
}