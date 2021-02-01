﻿using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using System;
ushort RatingValueResolver;
namespace Malzamaty
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            //Source -> Target
            CreateMap<Rating, RatingReadDto>();
            CreateMap<RatingWriteDto, Rating>();
            CreateMap<Rating, RatingWriteDto>();
        }
    }
}