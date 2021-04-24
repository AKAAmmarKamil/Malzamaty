using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Malzamaty.Services
{
    public interface IOrderService : IBaseService<Order, Guid>
    {
        Task<IEnumerable<Order>> GetAll(int OrderStatus);
    }

    public class OrderService : IOrderService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public OrderService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Order>> All(int PageNumber, int Count) => await _repositoryWrapper.Order.FindAll(PageNumber, Count);
        public Task<Order> Create(Order Order) =>
             _repositoryWrapper.Order.Create(Order);
        public Task<Order> Delete(Guid id) =>
        _repositoryWrapper.Order.Delete(id);

        public Task<Order> FindById(Guid id) =>
        _repositoryWrapper.Order.FindById(id);
        public async Task<IEnumerable<Order>> GetAll(int OrderStatus) => await _repositoryWrapper.Order.GetAll(OrderStatus);
        public async Task<Order> Modify(Guid id, Order Order)
        {
            var OrderModelFromRepo = await _repositoryWrapper.Order.FindById(id);
            if (OrderModelFromRepo == null)
            {
                return null;
            }
            OrderModelFromRepo.OrderStatus = Order.OrderStatus;
            _repositoryWrapper.Save();
            return Order;
        }

    }
}