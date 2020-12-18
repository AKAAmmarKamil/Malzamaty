using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class StageProfile : Profile
    {
        public StageProfile()
        {
            //Source -> Target
            CreateMap <Stage, StageReadDto> ();
            CreateMap <StageReadDto, Stage> ();
            CreateMap<Stage, StageWriteDto>();
            CreateMap<StageWriteDto, Stage>();

        }
    }
}