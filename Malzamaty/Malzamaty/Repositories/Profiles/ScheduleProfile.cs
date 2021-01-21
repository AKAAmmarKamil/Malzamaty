using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            //Source -> Target
            CreateMap<Schedule, ScheduleReadDto>()/*.ForMember(x => x.Description, opt => opt.MapFrom(x => x.File.Description))
                                              .ForMember(x => x., opt => opt.MapFrom(x => x.FileID))*/;
            CreateMap<ScheduleWriteDto, Schedule>().ForMember(x=>x.User,opt=>opt.Ignore()).ForMember(x => x.Subject, opt => opt.Ignore());
            CreateMap<Schedule, ScheduleWriteDto>();
        }
    }
}