using Business.Sales.Response.Products;
using Domain.Entities.Orders;
using System.Collections.Generic;

namespace Business.Sales.Response.Orders
{
    public class OrderRequest
    {
        public string Id { get; set; }
        public OrderStatusEnum Status { get; set; }
        public string CustomerId { get; set; }
        public ICollection<ProductResponse> Products { get; set; }
        public double Total { get; set; }
    }
}
