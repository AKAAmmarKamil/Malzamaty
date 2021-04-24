using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAll(int OrderStatus);
    }
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly MalzamatyContext _db;
        public OrderRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

        public async Task<IEnumerable<Order>> GetAll(int OrderStatus) => await _db.Order.Where(x => x.OrderStatus == OrderStatus).ToListAsync();
    }
}
