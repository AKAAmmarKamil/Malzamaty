using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            //Source -> Target
            CreateMap<Address, AddressReadDto>().ForMember(x => x.Country, opt => opt.MapFrom(x => x.Country.Name)).ForMember(x => x.Province, opt => opt.MapFrom(x => x.Province.Name))
            .ForMember(x => x.District, opt => opt.MapFrom(x => x.District.Name)).ForMember(x => x.Mahallah, opt => opt.MapFrom(x => x.Mahallah.Name));
            CreateMap<AddressWriteDto, Address>().ForMember(x => x.Country, opt => opt.MapFrom(x => x.CountryID)).ForMember(x => x.Province, opt => opt.MapFrom(x => x.ProvinceID))
                .ForMember(x => x.District, opt => opt.MapFrom(x => x.DistrictID)).ForMember(x => x.Mahallah, opt => opt.MapFrom(x=>x.MahallahID))
                .ForMember(x => x.Mahallah, opt => opt.Ignore())
                .ForMember(x => x.District, opt => opt.Ignore())
                .ForMember(x => x.Province, opt => opt.Ignore())
                .ForMember(x => x.Country, opt => opt.Ignore());
            CreateMap<Address, AddressWriteDto>();
        }
    }
}