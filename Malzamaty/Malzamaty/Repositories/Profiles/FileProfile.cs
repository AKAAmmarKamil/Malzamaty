using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            //Source -> Target
            CreateMap <File, FileReadDto > ().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                             .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                                             .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                             .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                             .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                             .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name));
            CreateMap<File, FileWithReportsReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                            .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                                            .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                            .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                            .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                            .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name));

            CreateMap<FileWriteDto, File>()
                .ForMember(x => x.UploadDate, opt => opt.MapFrom(x => System.DateTime.Now))
                .ForMember(x => x.Class, opt => opt.Ignore())
                .ForMember(x => x.Subject, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore()).ReverseMap()
                ;
                
            CreateMap<File,FileWriteDto >();
        }
    }
}