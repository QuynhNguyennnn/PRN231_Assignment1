using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DataAccess.repository;
using eStoreAPI.DTOs;
using BusinessObject;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IOrderRepository repository = new OrderRepository();


        public OrdersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrders() => repository.GetOrders();


        [HttpGet("id")]
        public ActionResult<Order> GetOrderDetail(int id)
        {
            return repository.GetOrderByID(id);
        }

        [HttpPost]
        public IActionResult PostOrder(AddOrderDtos o)
        {
            // Use mapper when post
            var orderDto = _mapper.Map<Order>(o);
            repository.InsertOrder(orderDto);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteOrder(int id)
        {
            var o = repository.GetOrderByID(id);
            if (o == null)
            {
                return NotFound();
            }
            repository.DeleteOrder(o);
            return Ok(repository.GetOrders());
        }

        [HttpPut("id")]
        public IActionResult UpdateOrder(int id, Order o)
        {
            var oImp = repository.GetOrderByID(id);
            if (o == null)
                return NotFound();
            var orderDto = _mapper.Map<Order>(o);
            repository.UpdateOrder(orderDto);
            return NoContent();
        }
        [HttpPost("sortOrder")]
        public ActionResult<List<Order>> SortOrder(SortDto sortDto) => repository.SortOrder(sortDto.DateStart, sortDto.DateEnd);
    }
}
