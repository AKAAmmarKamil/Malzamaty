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
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IAddressService addressService,IUserService userService,IFileService fileService, IMapper mapper)
        {
            _orderService = orderService;
            _addressService = addressService;
            _userService = userService;
            _fileService = fileService;
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
            result.File = await _fileService.FindById(result.FileID);
            var OrderModel = _mapper.Map<OrderReadDto>(result);
            var User = await _userService.GetUserByAddress(result.UserAddressID);
            var IsBestCustomer = await _orderService.IsBestCustomer(User.ID);
            if (IsBestCustomer)
                OrderModel.IsBestCustomer = true;
            OrderModel.File = _mapper.Map<FileReadDto>(result.File);
            return Ok(OrderModel);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.DeliveryAdmin + "," + UserRole.DeliveryRepresentative)]
        public async Task<ActionResult<OrderReadDto>> GetAllOrders(int OrderStatus)
        {
            var result = _orderService.GetAll(OrderStatus).Result.ToList();
            var OrderModel = _mapper.Map<IList<OrderReadDto>>(result);
            for (int i = 0; i < result.Count; i++)
            {
                if (GetClaim("Role") == "DeliveryRepresentative" && result[i].OrderStatus == 1)
                {
                    return BadRequest(new { Error = "لايمكن للمندوب الإطلاع على الطلبات التي تم تسليمها" });
                }
                result[i].LibraryAddress = await _addressService.FindById(result[i].LibraryAddressID);
                result[i].UserAddress = await _addressService.FindById(result[i].UserAddressID);
                result[i].File = await _fileService.FindById(result[i].FileID);
                OrderModel[i].From = _mapper.Map<AddressReadDto>(result[i].LibraryAddress);
                OrderModel[i].To = _mapper.Map<AddressReadDto>(result[i].UserAddress);
                var User = await _userService.GetUserByAddress(result[i].UserAddressID);
                var IsBestCustomer = await _orderService.IsBestCustomer(User.ID);
                if (IsBestCustomer)
                    OrderModel[i].IsBestCustomer = true;
                OrderModel[i].File = _mapper.Map<FileReadDto>(result[i].File);
            }
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