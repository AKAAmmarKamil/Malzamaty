using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
namespace Malzamaty.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            //Source -> Target
            CreateMap<Library, LibraryReadDto>();
            CreateMap<AddressReadDto, LibraryReadDto>();
            CreateMap<LibraryWriteDto, Library>();
            CreateMap<Library, LibraryWriteDto>();
        }
    }
}
