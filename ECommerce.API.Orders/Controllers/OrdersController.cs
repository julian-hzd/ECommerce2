using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]

    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderAsync(int id)
        {
            var result = await ordersProvider.GetOrdersAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

    }
}
