﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Persistence;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _orderRepository.GetOrdersAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("{orderId}", Name="GetByOrderId")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var order = await _orderRepository.GetOrderAsync(Guid.Parse(orderId));
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
