using Business.Sales.Interfaces;
using Business.Sales.Response.Orders;
using Business.Sales.Response.Products;
using Domain.Data;
using Domain.Data.ContextRepository;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Sales.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRespository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderService(
            IRepository<Order> orderRespository,
            IRepository<Customer> customerRepository,
            IRepository<Product> productRepository)
        {
            _orderRespository = orderRespository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task AddOrder(OrderRequest orderRequest)
        {
            var customer = await _customerRepository.GetByIdAsync(orderRequest.CustomerId);

            await _orderRespository.InsertAsync(new Order
            {
                Total = orderRequest.Products.Sum(s => s.Price),
                Products = orderRequest.Products.Select(s =>
                    new Product
                    {
                        Name = s.Name,
                        Price = s.Price
                    }).ToList(),
                Status = orderRequest.Status,
                Customer = new Customer
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Active = customer.Active
                },
                CreatedOnUtc = new DateTime(2021, 12, 23)
            });
        }

        public async Task<List<OrderResponse>> GetOrders()
        {
            var orders = await _orderRespository.GetAllAsync();
            var toResponse = orders.Select(s => new OrderResponse
            {
                CustomerId = s.Customer.Id,
                Customer = s.Customer.Name,
                Status = s.Status.ToString(),
                Total = s.Total,
                Products = s.Products.Select(s =>
                    new ProductResponse
                    {
                        Name = s.Name,
                        Price = s.Price
                    }).ToList()
            }).ToList();
            return toResponse;
        }

        public async Task<bool> RemoveOrder(string id)
        {
            try
            {
                await _orderRespository.DeleteManyAsync(w => w.Id.Equals(id));
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        public async Task<object> GetStatus()
        {
            var status =
                Enum.GetValues(typeof(OrderStatusEnum))
                    .Cast<OrderStatusEnum>()
                    .ToDictionary(key => (int)key, value => value.ToString())
                    .Select(s => new { id = s.Key, name = s.Value}).ToList();

            return await Task.FromResult(status);
        }

        public async Task<object> GetCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(s => new { id = s.Id, name = s.Name }).ToList();
        }

        public async Task<bool> UpdateOrder(OrderRequest orderRequest)
        {
            try
            {
                if (orderRequest == null)
                    throw new ArgumentNullException(nameof(orderRequest));

                var customer = await _customerRepository.GetByIdAsync(orderRequest.Id);

                var productsId = orderRequest.Products.Select(s => s.Id);
                var products = await _productRepository.GetAllAsync(w => productsId.Contains(w.Id));

                var update = UpdateBuilder<Order>.Create()
                    .Set(x => x.Customer, customer)
                    .Set(x => x.Products, products)
                    .Set(x => x.Status, orderRequest.Status)
                    .Set(x => x.Total, orderRequest.Total);

                await _orderRespository.UpdateOneAsync(x => x.Id.Equals(orderRequest.Id), update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
