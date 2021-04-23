using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class MahallahProfile : Profile
    {
        public MahallahProfile()
        {
            //Source -> Target
            CreateMap<Mahallah, MahallahReadDto>().ForMember(x => x.District, opt => opt.MapFrom(x => x.District.Name));
            CreateMap<MahallahWriteDto, Mahallah>().ForMember(x => x.DistrictID, opt => opt.MapFrom(x => x.District))
            .ForMember(x => x.District, opt => opt.Ignore());
            CreateMap<Mahallah, MahallahWriteDto>();
        }
    }
}