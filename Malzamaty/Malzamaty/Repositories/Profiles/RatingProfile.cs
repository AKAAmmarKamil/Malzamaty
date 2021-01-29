using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using System;

namespace Malzamaty
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            //Source -> Target
            CreateMap<Rating, RatingReadDto>();
            CreateMap<RatingWriteDto, Rating>().ForMember(d => d.File,
                 opt => opt.MapFrom(s => s));
            CreateMap<Rating, RatingWriteDto>();
        }
    }
}