using Business.Customers.Interfaces;
using Business.Customers.Requests;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NetReactTemplateAdmin.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetCustomers")]
        public async Task<ActionResult> GetCustomers()
        {
            return Ok(await _customerService.GetAll());
        }

        [HttpGet("GetCustomersByName")]
        public async Task<ActionResult> GetCustomersByName(string name)
        {
            return Ok(await _customerService.GetByName(name));
        }

        [HttpDelete("RemoveCustomer")]
        public ActionResult RemoveCustomer([FromBody] string customerId)
        {
            _customerService.Delete(customerId);
            return Ok();
        }

        [HttpPost("AddCustomer")]
        public async Task<ActionResult> AddCustomer([FromBody] CustomerRequest customerRequest)
        {
            return Ok(await _customerService.Add(customerRequest));
        }

        [HttpPost("UpdateCustomer")]
        public async Task<ActionResult> UpdateCustomer([FromBody] CustomerRequest customerRequest)
        {
            return Ok(await _customerService.Update(customerRequest));
        }
    }
}
