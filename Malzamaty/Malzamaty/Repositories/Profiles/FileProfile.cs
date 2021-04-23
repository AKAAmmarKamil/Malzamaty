using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using System;
using System.Linq;
namespace Malzamaty
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            //Source -> Target
            CreateMap<File, FileReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                             .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName))
                                             .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                             .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                             .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                             .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name));
            CreateMap<File, FileWithReportsReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                            .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName))
                                            .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                            .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                            .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                            .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name));
            CreateMap<File, FileWithReportsAndRatingReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                          .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName))
                                          .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                          .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                          .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                          .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name))
                                          .ForMember(x => x.Rate, opt => opt.MapFrom(x => x.Rating.Average(a => a.Rate)));
            CreateMap<File, FileWithRatingReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                                      .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName))
                                                      .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                                      .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                                      .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                                      .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name))
                                                      .ForMember(x => x.Rate, opt => opt.MapFrom(x => x.Rating.Average(a => a.Rate)));
            CreateMap<FileWriteDto, File>()
                .ForMember(x => x.UploadDate, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(x => x.Class,opt=>opt.Ignore())
                .ForMember(x => x.Author, opt => opt.Ignore())
                .ForMember(source => source.Subject, opt => opt.Ignore());
            CreateMap<File, FileWriteDto>();
        }
    }
}