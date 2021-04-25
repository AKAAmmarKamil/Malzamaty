using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;

namespace Malzamaty
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            //Source -> Target
            CreateMap<Order, OrderReadDto>().ForMember(x => x.From, opt => opt.MapFrom(x => x.LibraryAddress)).ForMember(x => x.To, opt => opt.MapFrom(x => x.UserAddress));
            CreateMap<Order, OrderFileReadDto>().ForMember(x => x.To, opt => opt.MapFrom(x => x.UserAddress));
            CreateMap<OrderUpdateDto, Order>().ForMember(x => x.LastUpdateDate, opt => opt.MapFrom(x=>System.DateTime.Now));

        }
    }
}