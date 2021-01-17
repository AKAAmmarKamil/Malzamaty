using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            //Source -> Target
            CreateMap<Report, ReportReadDto>()/*.ForMember(x => x.Description, opt => opt.MapFrom(x => x.File.Description))
                                              .ForMember(x => x.File, opt => opt.MapFrom(x => x.File))*/;                                              
            CreateMap<ReportWriteDto, Report>().ForMember(x => x.Date, opt => opt.MapFrom(x => System.DateTime.Now));
            CreateMap<Report, ReportWriteDto>();
        }
    }
}