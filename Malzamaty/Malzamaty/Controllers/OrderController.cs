using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IAddressService addressService, IMapper mapper)
        {
            _orderService = orderService;
            _addressService = addressService;
            _mapper = mapper;
        }
        [HttpGet("{Id}", Name = "GetOrderById")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.DeliveryAdmin + "," + UserRole.DeliveryRepresentative)]
        public async Task<ActionResult<OrderReadDto>> GetOrderById(Guid Id)
        {
            var result = await _orderService.FindById(Id);
            
            if (GetClaim("Role") == "DeliveryRepresentative" && result.OrderStatus == 1)
            {
                return BadRequest(new { Error = "لايمكن للمندوب الإطلاع على الطلبات التي تم تسليمها" });
            }
            if (result == null)
            {
                return NotFound();
            }
            result.LibraryAddress = await _addressService.FindById(result.LibraryAddressID);
            result.UserAddress = await _addressService.FindById(result.UserAddressID);
            var OrderModel = _mapper.Map<OrderReadDto>(result);
            return Ok(OrderModel);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.DeliveryAdmin + "," + UserRole.DeliveryRepresentative)]
        public async Task<ActionResult<OrderReadDto>> GetAllOrders(int OrderStatus)
        {
            var result = _orderService.GetAll(OrderStatus).Result.ToList();
            for (int i = 0; i < result.Count; i++)
            {
                if (GetClaim("Role") == "DeliveryRepresentative" && result[i].OrderStatus == 1)
                {
                    return BadRequest(new { Error = "لايمكن للمندوب الإطلاع على الطلبات التي تم تسليمها" });
                }
                result[i].LibraryAddress = await _addressService.FindById(result[i].LibraryAddressID);
                result[i].UserAddress = await _addressService.FindById(result[i].UserAddressID);
            }
            var OrderModel = _mapper.Map<IList<OrderReadDto>>(result);
            return Ok(OrderModel);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.DeliveryAdmin + "," + UserRole.DeliveryRepresentative)]
        public async Task<IActionResult> UpdateOrder(Guid Id, [FromBody] OrderUpdateDto OrderUpdateDto)
        {
            var OrderModelFromRepo = await _orderService.FindById(Id);
            if (OrderModelFromRepo == null)
            {
                return NotFound();
            }
            var OrderModel = _mapper.Map<Order>(OrderUpdateDto);
            await _orderService.Modify(Id, OrderModel);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteOrders(Guid Id)
        {
            var Order = await _orderService.Delete(Id);
            if (Order == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}