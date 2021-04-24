using AutoMapper;
using Malzamaty.Form;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAll(int OrderStatus);
        Task<bool> IsBestCustomer(Guid Id);

    }
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly MalzamatyContext _db;
        public OrderRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

        public async Task<IEnumerable<Order>> GetAll(int OrderStatus) => await _db.Order.Where(x => x.OrderStatus == OrderStatus).ToListAsync();

        public async Task<bool> IsBestCustomer(Guid Id)
        {
            var Result = _db.Order.Include(x => x.UserAddress).ThenInclude(x => x.User).Where(x => x.OrderStatus == 1)
                     .GroupBy(x => x.UserAddress.User.ID).Select(x => new BestCustomer { User=x.Key,Total = x.Count() }).OrderByDescending(x => x.Total).Take(1).ToListAsync();     
            if (Result.Result[0].User==Id)
            {
                return true;
            }
            return false;
        }
    }
}
