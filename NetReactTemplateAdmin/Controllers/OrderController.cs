using Business.Sales.Interfaces;
using Business.Sales.Response.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetReactTemplateAdmin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderController(
            IOrderService orderService,
            IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody] OrderRequest orderRequest)
        {
            await _orderService.AddOrder(orderRequest);
            return Ok();
        }

        [HttpDelete("RemoveOrder")]
        public async Task<ActionResult> RemoveOrder([FromBody] string orderId)
        {
            await _orderService.RemoveOrder(orderId);
            return Ok();
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _orderService.GetOrders());
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("GetStatus")]
        public async Task<ActionResult> GetStatus()
        {
            return Ok(await _orderService.GetStatus());
        }

        [HttpGet("GetCustomers")]
        public async Task<ActionResult> GetCustomers()
        {
            return Ok(await _orderService.GetCustomers());
        }

        [HttpPost("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder([FromBody] OrderRequest orderRequest)
        {
            return Ok(await _orderService.UpdateOrder(orderRequest));
        }
    }
}
