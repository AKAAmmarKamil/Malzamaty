using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            //Source -> Target
            CreateMap<Rating, RatingReadDto>()/*.ForMember(x => x.Description, opt => opt.MapFrom(x => x.File.Description))
                                              .ForMember(x => x., opt => opt.MapFrom(x => x.FileID))*/;
            CreateMap<RatingWriteDto, Rating>();
            CreateMap<Rating, RatingWriteDto>();
        }
    }
}