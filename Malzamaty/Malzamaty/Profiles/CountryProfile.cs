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
            CreateMap <Country, CountryReadDto> ();
            CreateMap <CountryReadDto, Country> ();
            CreateMap<Country, CountryWriteDto>();
            CreateMap<CountryWriteDto, Country>();

        }
    }
}