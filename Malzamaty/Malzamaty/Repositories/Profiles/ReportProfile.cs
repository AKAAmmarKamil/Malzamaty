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
            CreateMap<Report, ReportReadDto>();
            CreateMap<ReportWriteDto, Report>().ForMember(x => x.Date, opt => opt.MapFrom(x => System.DateTime.Now));
            CreateMap<Report, ReportWriteDto>();
        }
    }
}