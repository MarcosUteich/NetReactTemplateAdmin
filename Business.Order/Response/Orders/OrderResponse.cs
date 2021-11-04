using Business.Sales.Response.Products;
using System.Collections.Generic;

namespace Business.Sales.Response.Orders
{
    public class OrderResponse
    {
        public string Status { get; set; }
        public string CustomerId { get; set; }
        public string Customer { get; set; }
        public ICollection<ProductResponse> Products { get; set; }
        public double Total { get; set; }
    }
}
