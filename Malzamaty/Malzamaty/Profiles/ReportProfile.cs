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
            CreateMap<Report, ReportReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.File.Description))
                                              .ForMember(x => x.FilePath, opt => opt.MapFrom(x => x.File.FilePath))
                                              .ForMember(x => x.Author, opt => opt.MapFrom(x => x.File.Author))
                                              .ForMember(x => x.Type, opt => opt.MapFrom(x => x.File.Type))
                                              .ForMember(x => x.Format, opt => opt.MapFrom(x => x.File.Format))
                                              .ForMember(x => x.PublishDate, opt => opt.MapFrom(x => x.File.PublishDate))
                                              .ForMember(x => x.DownloadCount, opt => opt.MapFrom(x => x.File.DownloadCount))
                                              .ForMember(x => x.Class, opt => opt.MapFrom(x => x.File.Class.Name))
                                              .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.File.Class.Stage.Name))
                                              .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.File.Class.ClassType.Name))
                                              .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.File.User.UserName))
                                              .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.File.Subject.Name));

            CreateMap<ReportWriteDto, Report>().ForMember(x => x.Date, opt => opt.MapFrom(x => System.DateTime.Now));
            CreateMap<Report, ReportWriteDto>();
        }
    }
}