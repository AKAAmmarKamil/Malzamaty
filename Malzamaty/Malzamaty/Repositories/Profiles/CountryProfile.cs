using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            //Source -> Target
            CreateMap<Country, CountryReadDto>();
            CreateMap<CountryWriteDto, Country>();
            CreateMap<Country, CountryWriteDto>();
        }
    }
}