using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class TaxesProfile : Profile
    {
        public TaxesProfile()
        {
            //Source -> Target
            CreateMap<TaxesWriteDto, Taxes>();
            CreateMap<Taxes, TaxesWriteDto>();
        }
    }
}