using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
using Talabat.Core.Entities.Order;
using Talabat.Core.Interfaces;
using Talabat.Core.Services;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService service, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _service = service;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto order)
        {
            var Order = await _service.CreateOrder(order.BuyerEmail, order.BasketId, order.DeliveryMethodId, order.ShippingAddress);



            if (Order == null)
                return BadRequest(404);

            return Ok(Order);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersByUser()
        {

            var Email = User.FindFirstValue(ClaimTypes.Email);

            var Orders = await _service.GetOrdersForSpecificUser(Email);

            var OrdersMapp = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
            if (OrdersMapp == null)
                return NotFound(new ApiResponse(404));


            return Ok(OrdersMapp);
        }


        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOrdersByUser(int id)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _service.GetOrderByIdForSpecificUser(Email, id);

            var ordermapp = _mapper.Map<Order, OrderToReturnDto>(order);
            if (ordermapp == null)
                return NotFound(new ApiResponse(404));

            return Ok(ordermapp);
        }

        [HttpGet("GetAllDelivery")]
        public async Task<IEnumerable<DeliveryMethod>> GetAllDelivery()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }
    }
}
