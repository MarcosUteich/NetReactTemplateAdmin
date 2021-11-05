using Business.Sales.Interfaces;
using Business.Sales.Request.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetReactTemplateAdmin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] ProductRequest productRequest)
        {
            await _productService.Add(productRequest);
            return Ok();
        }

        [HttpDelete("RemoveProduct")]
        public async Task<ActionResult> RemoveProduct([FromBody] string productId)
        {
            await _productService.Delete(productId);
            return Ok();
        }

        [HttpPost("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductRequest productRequest)
        {
            await _productService.Update(productRequest);
            return Ok();
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("GetProductsByName")]
        public async Task<ActionResult> GetProductsByName([FromQuery] string name)
        {
            return Ok(await _productService.GetByName(name));
        }
    }
}
