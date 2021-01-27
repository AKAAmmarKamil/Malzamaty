using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Repositories.Profiles;
using Malzamaty.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            CreateMap<File, FileWithReportsAndRatingReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                          .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                                          .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                          .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                          .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                          .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name))
                                          .ForMember(x => x.Rate, opt => opt.MapFrom(x => x.Rating.Average(a => a.Rate)));
            CreateMap<File, FileWithRatingReadDto>().ForMember(x => x.FileDescription, opt => opt.MapFrom(x => x.Description))
                                                      .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                                                      .ForMember(x => x.ClassType, opt => opt.MapFrom(x => x.Class.ClassType.Name))
                                                      .ForMember(x => x.Stage, opt => opt.MapFrom(x => x.Class.Stage.Name))
                                                      .ForMember(x => x.ClassName, opt => opt.MapFrom(x => x.Class.Name))
                                                      .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name))
                                                      .ForMember(x => x.Rate, opt => opt.MapFrom(x=>x.Rating.Average(a=>a.Rate)));
            CreateMap<FileWriteDto, File>()
                .ForMember(x => x.UploadDate, opt => opt.MapFrom(x => System.DateTime.Now))
                .ForMember(x => x.Class, opt => opt.Ignore())
                .ForMember(x => x.Subject, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore())
                ;
                
            CreateMap<File,FileWriteDto >();
           

        }
    }
}