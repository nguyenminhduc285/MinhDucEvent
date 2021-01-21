using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.Application.Catalog.Categories;
using MinhDucEvent.Application.Catalog.Orders;
using MinhDucEvent.ViewModels.Catalog.Categories;
using MinhDucEvent.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _oderService;

        public OrdersController(
            IOrderService orderService)
        {
            _oderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _oderService.GetById(id);
            return Ok(order);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageOrderPagingRequest request)
        {
            var orders = await _oderService.GetAllPaging(request);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CheckoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderId = await _oderService.Create(request);
            if (orderId == 0)
            {
                return BadRequest();
            }
            var ca = await _oderService.GetById(orderId);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, ca);
        }
    }
}