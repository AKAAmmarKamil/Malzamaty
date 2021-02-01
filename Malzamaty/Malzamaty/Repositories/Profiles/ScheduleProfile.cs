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
            CreateMap<Schedule, ScheduleReadDto>();
            CreateMap<ScheduleWriteDto, Schedule>().ForMember(x => x.User, opt => opt.Ignore()).ForMember(x => x.Subject, opt => opt.Ignore());
            CreateMap<Schedule, ScheduleWriteDto>();
        }
    }
}